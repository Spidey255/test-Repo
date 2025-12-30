import React, { useEffect, useRef, useState } from "react";
import "./FuturisticSchedulerGrid.css";

/*
Event model:
{
  id: string,
  title: string,
  startDate: 'YYYY-MM-DD',
  endDate: 'YYYY-MM-DD', // multi-day support
  resourceId: string,
  start: minutesFromMidnight (0-1440),
  end: minutesFromMidnight,
  color: cssBackgroundString
}
*/

const DEFAULT_RESOURCES = [
  { id: "r1", name: "Room A" },
  { id: "r2", name: "Room B" },
  { id: "r3", name: "Team 1" },
];

const SLOT_MINUTES = 30;
const START_HOUR = 8;
const END_HOUR = 20;

function todayISO(offsetDays = 0) {
  const d = new Date();
  d.setDate(d.getDate() + offsetDays);
  return d.toISOString().slice(0, 10);
}

function formatTime(minutes) {
  const h = Math.floor(minutes / 60);
  const m = minutes % 60;
  return `${String(h).padStart(2, "0")}:${String(m).padStart(2, "0")}`;
}

function clamp(v, a, b) {
  return Math.max(a, Math.min(b, v));
}

export default function FuturisticSchedulerGrid({
  initialResources = DEFAULT_RESOURCES,
  initialEvents = null,
  startHour = START_HOUR,
  endHour = END_HOUR,
  slotMinutes = SLOT_MINUTES,
}) {
  const slotsPerHour = 60 / slotMinutes;
  const totalSlots = (endHour - startHour) * slotsPerHour;

  const [resources, setResources] = useState(initialResources);
  const [events, setEvents] = useState(() => {
    try {
      const raw = localStorage.getItem("scheduler.events.v2");
      if (raw) return JSON.parse(raw);
    } catch {}
    if (initialEvents && initialEvents.length) return initialEvents;
    return [
      { id: "ev1", title: "Team standup", startDate: todayISO(0), endDate: todayISO(0), resourceId: "r1", start: 9 * 60, end: 9 * 60 + 30, color: "linear-gradient(135deg,#6EE7B7,#60A5FA)" },
      { id: "ev2", title: "Client call", startDate: todayISO(1), endDate: todayISO(1), resourceId: "r2", start: 11 * 60, end: 12 * 60, color: "linear-gradient(135deg,#FDBA74,#F472B6)" },
    ];
  });

  const [selectedResourceIds, setSelectedResourceIds] = useState([initialResources[0]?.id ?? null]);
  const [slotHeight, setSlotHeight] = useState(36);
  const [viewMode, setViewMode] = useState("week"); // day | week | workweek
  const [anchorDate, setAnchorDate] = useState(todayISO(0));
  const [selectedEventId, setSelectedEventId] = useState(null);
  const [showEditor, setShowEditor] = useState(false);
  const [editorData, setEditorData] = useState(null);
  const [dragState, setDragState] = useState(null);
  const containerRef = useRef(null);

  const days = (() => {
    if (viewMode === "day") return [anchorDate];
    if (viewMode === "workweek") {
      const base = new Date(anchorDate);
      const day = base.getDay();
      const diff = (day + 6) % 7;
      const monday = new Date(base);
      monday.setDate(base.getDate() - diff);
      return Array.from({ length: 5 }).map((_, i) => {
        const d = new Date(monday);
        d.setDate(monday.getDate() + i);
        return d.toISOString().slice(0, 10);
      });
    }
    const start = new Date(anchorDate);
    return Array.from({ length: 7 }).map((_, i) => {
      const d = new Date(start);
      d.setDate(start.getDate() + i);
      return d.toISOString().slice(0, 10);
    });
  })();

  useEffect(() => {
    try {
      localStorage.setItem("scheduler.events.v2", JSON.stringify(events));
    } catch {}
  }, [events]);

  useEffect(() => {
    function onKey(e) {
      if ((e.key === "Delete" || e.key === "Backspace") && selectedEventId) {
        setEvents((p) => p.filter((x) => x.id !== selectedEventId));
        setSelectedEventId(null);
      }
    }
    window.addEventListener("keydown", onKey);
    return () => window.removeEventListener("keydown", onKey);
  }, [selectedEventId]);

  function minutesToY(minutes) {
    const slotsFromStart = (minutes - startHour * 60) / slotMinutes;
    return slotsFromStart * slotHeight;
  }

  function yToMinutes(y) {
    const slots = Math.round(y / slotHeight);
    return clamp(startHour * 60 + slots * slotMinutes, startHour * 60, endHour * 60);
  }

  function onCellMouseDown(e, day, resourceId) {
    if (e.button !== 0) return;
    const rect = containerRef.current.getBoundingClientRect();
    const y = e.clientY - rect.top + containerRef.current.scrollTop;
    const minute = yToMinutes(y);
    setDragState({
      type: "create",
      startY: y,
      startMinutes: minute,
      currentMinutes: minute,
      day,
      resourceId,
      currentDay: day,
    });
  }

  function onEventMouseDown(e, ev) {
    e.stopPropagation();
    const rect = containerRef.current.getBoundingClientRect();
    const startY = e.clientY - rect.top + containerRef.current.scrollTop;
    setDragState({
      type: "move",
      eventId: ev.id,
      original: { ...ev },
      startY,
      startMinutes: ev.start,
      currentMinutes: ev.start,
      day: ev.startDate,
      resourceId: ev.resourceId,
      currentDay: ev.startDate,
    });
  }

  function onEventResizeMouseDown(e, ev, direction) {
    e.stopPropagation();
    const rect = containerRef.current.getBoundingClientRect();
    const startY = e.clientY - rect.top + containerRef.current.scrollTop;
    setDragState({
      type: "resize",
      eventId: ev.id,
      direction,
      original: { ...ev },
      startY,
      startMinutes: ev.start,
      endMinutes: ev.end,
      day: ev.startDate,
      resourceId: ev.resourceId,
      currentDay: ev.startDate,
    });
  }

  // Drag & touch handling
  useEffect(() => {
    if (!dragState) return;
    if (!containerRef.current) return;
    const rect = containerRef.current.getBoundingClientRect();

    function updateLive(e) {
      const clientY = e.touches ? e.touches[0].clientY : e.clientY;
      const clientX = e.touches ? e.touches[0].clientX : e.clientX;
      const x = clientX - rect.left + containerRef.current.scrollLeft;
      const y = clientY - rect.top + containerRef.current.scrollTop;

      const daysCount = days.length;
      const dayColumnWidth = Math.max(180, (rect.width - 120) / daysCount);
      const dayIndex = clamp(Math.floor((x - 120) / (dayColumnWidth + 8)), 0, daysCount - 1);
      const day = days[dayIndex];

      const relY = y;
      const minutesFromTop = Math.round(relY / slotHeight) * slotMinutes;
      const minutes = clamp(startHour * 60 + minutesFromTop, startHour * 60, endHour * 60);

      if (dragState.type === "create") {
        setDragState((s) => ({ ...s, currentMinutes: minutes, currentDay: day }));
      } else if (dragState.type === "move") {
        const deltaSlots = Math.round((y - dragState.startY) / slotHeight);
        const deltaMins = deltaSlots * slotMinutes;
        setEvents((prev) =>
          prev.map((p) =>
            p.id === dragState.eventId
              ? {
                  ...p,
                  start: clamp(dragState.original.start + deltaMins, startHour * 60, endHour * 60 - (dragState.original.end - dragState.original.start)),
                  end: clamp(dragState.original.end + deltaMins, startHour * 60 + (dragState.original.end - dragState.original.start), endHour * 60),
                  startDate: day,
                  endDate: day,
                }
              : p
          )
        );
        setDragState((s) => ({ ...s, currentMinutes: minutes, currentDay: day }));
      } else if (dragState.type === "resize") {
        if (dragState.direction === "bottom") {
          setEvents((prev) =>
            prev.map((p) => (p.id === dragState.eventId ? { ...p, end: clamp(minutes, p.start + slotMinutes, endHour * 60) } : p))
          );
        } else {
          setEvents((prev) =>
            prev.map((p) => (p.id === dragState.eventId ? { ...p, start: clamp(minutes, startHour * 60, p.end - slotMinutes) } : p))
          );
        }
        setDragState((s) => ({ ...s, currentMinutes: minutes, currentDay: day }));
      }
    }

    function endDrag() {
      if (!dragState) return;
      if (dragState.type === "create") {
        const start = Math.min(dragState.startMinutes, dragState.currentMinutes);
        const end = Math.max(dragState.startMinutes, dragState.currentMinutes) + slotMinutes;
        const ev = {
          id: `ev-${Date.now()}`,
          title: "New Event",
          startDate: dragState.currentDay || dragState.day,
          endDate: dragState.currentDay || dragState.day,
          resourceId: dragState.resourceId,
          start: clamp(start, startHour * 60, endHour * 60 - slotMinutes),
          end: clamp(end, startHour * 60 + slotMinutes, endHour * 60),
          color: ["linear-gradient(135deg,#6EE7B7,#60A5FA)","linear-gradient(135deg,#FDBA74,#F472B6)","linear-gradient(135deg,#C084FC,#60A5FA)"][Math.floor(Math.random()*3)]
        };
        setEvents((p) => [...p, ev]);
      }
      setDragState(null);
    }

    window.addEventListener("mousemove", updateLive);
    window.addEventListener("mouseup", endDrag);
    window.addEventListener("touchmove", updateLive);
    window.addEventListener("touchend", endDrag);
    return () => {
      window.removeEventListener("mousemove", updateLive);
      window.removeEventListener("mouseup", endDrag);
      window.removeEventListener("touchmove", updateLive);
      window.removeEventListener("touchend", endDrag);
    };
  }, [dragState, days, slotHeight, slotMinutes, startHour, endHour]);

  function openEditor(ev) {
    setEditorData({ ...ev });
    setShowEditor(true);
  }

  function saveEditor() {
    setEvents((p) => p.map((x) => (x.id === editorData.id ? editorData : x)));
    setShowEditor(false);
    setEditorData(null);
  }

  function exportJSON() {
    const payload = { events, resources };
    navigator.clipboard?.writeText(JSON.stringify(payload, null, 2));
    alert("Schedule JSON copied to clipboard");
  }

  function importJSON(raw) {
    try {
      const data = JSON.parse(raw);
      if (Array.isArray(data.events)) setEvents(data.events);
      if (Array.isArray(data.resources)) setResources(data.resources);
      alert("Imported successfully");
    } catch (err) {
      alert("Invalid JSON");
    }
  }

  const cellHeight = totalSlots * slotHeight;

  return (
    <div className="d-flex gap-3 flex-wrap">
      {/* Sidebar */}
      <div className="scheduler-sidebar card p-2">
        <h5>Scheduler</h5>

        {/* View Mode */}
        <div className="btn-group mb-2">
          <button className={`btn ${viewMode==="day"?"btn-primary":"btn-outline-light"}`} onClick={()=>setViewMode("day")}>Day</button>
          <button className={`btn ${viewMode==="week"?"btn-primary":"btn-outline-light"}`} onClick={()=>setViewMode("week")}>Week</button>
          <button className={`btn ${viewMode==="workweek"?"btn-primary":"btn-outline-light"}`} onClick={()=>setViewMode("workweek")}>Work-week</button>
        </div>

        {/* Anchor Date */}
        <div className="mb-2">
          <input type="date" className="form-control form-control-sm" value={anchorDate} onChange={e=>setAnchorDate(e.target.value)}/>
        </div>

        {/* Resource Selection */}
        <div className="mb-2">
          <label className="form-label small">Select Resource(s)</label>
          <select className="form-select form-select-sm" multiple value={selectedResourceIds} onChange={e=>{
            const options = Array.from(e.target.selectedOptions).map(opt=>opt.value);
            setSelectedResourceIds(options);
          }}>
            {resources.map(r => <option key={r.id} value={r.id}>{r.name}</option>)}
          </select>
        </div>

        {/* Quick Add / Remove Resources */}
        <div className="list-group mb-2">
          {resources.map(r=>(
            <div key={r.id} className="list-group-item list-group-item-dark d-flex justify-content-between align-items-center">
              <small>{r.name}</small>
              <div>
                <button className="btn btn-sm btn-outline-light me-1" onClick={()=>{
                  const ev = { id:`ev-${Date.now()}`, title:"Quick event", startDate:todayISO(0), endDate:todayISO(0), resourceId:r.id, start:14*60, end:15*60, color:"linear-gradient(135deg,#C084FC,#60A5FA)"}; 
                  setEvents(p=>[...p,ev]);
                }}>+</button>
                <button className="btn btn-sm btn-outline-danger" onClick={()=>setResources(rs=>rs.filter(x=>x.id!==r.id))}>🗑</button>
              </div>
            </div>
          ))}
        </div>
        <div className="input-group input-group-sm mb-2">
          <input id="newResourceName" className="form-control form-control-sm" placeholder="New resource name"/>
          <button className="btn btn-sm btn-success" onClick={()=>{
            const el=document.getElementById("newResourceName"); if(!el.value)return;
            setResources(r=>[...r,{id:`r${Date.now()}`, name:el.value}]);
            el.value="";
          }}>Add</button>
        </div>

        {/* Import/Export */}
        <div className="mt-3">
          <button className="btn btn-sm btn-outline-light w-100 mb-1" onClick={exportJSON}>Export</button>
          <button className="btn btn-sm btn-outline-light w-100" onClick={()=>{const raw=prompt("Paste schedule JSON"); if(raw)importJSON(raw)}}>Import</button>
        </div>
      </div>

      {/* Main Scheduler */}
      <div className="scheduler-main card p-2" style={{minWidth:700, overflowX:"auto"}}>
        <div className="d-flex gap-2 align-items-center mb-2">
          <h6>{viewMode==="day"?"Day view":viewMode==="workweek"?"Work-week view":"Week view"}</h6>
          <small className="text-muted">• {days.length} days</small>
        </div>

        {/* Day Headers */}
        <div className="days-header mb-2" style={{display:"grid", gridTemplateColumns:`repeat(${days.length}, minmax(180px,1fr))`, gap:"8px"}}>
          {days.map(d=>(<div key={d} className="day-pill p-1 text-center" >
            <strong style={{fontSize:13}}>{new Date(d).toLocaleDateString(undefined,{weekday:"short"})}</strong>
            <div style={{fontSize:12,color:"#9fb1c9"}}>{d}</div>
          </div>))}
        </div>

        {/* Grid */}
        <div className="grid d-flex" ref={containerRef} onMouseDown={()=>setSelectedEventId(null)} style={{position:"relative"}}>
          {/* Time gutter */}
          <div style={{width:120}}>
            {Array.from({length:totalSlots}).map((_,i)=>{
              const minute = startHour*60+i*slotMinutes;
              const showLabel=i%slotsPerHour===0;
              return <div key={i} style={{height:slotHeight, fontSize:12, color:"#9fb1c9"}}>{showLabel?formatTime(minute):null}</div>
            })}
          </div>

          {/* Columns */}
          <div className="day-columns d-flex" style={{gap:8}}>
            {days.map(day=>(
              <div key={day} className="d-flex flex-column gap-1" style={{minWidth:180}}>
                {resources.filter(r=>selectedResourceIds.includes(r.id)).map(res=>(
                  <div key={res.id+"|"+day} className="position-relative" style={{height:cellHeight, borderRadius:4, cursor:"pointer"}} onMouseDown={e=>onCellMouseDown(e,day,res.id)}>
                    {/* Events */}
                    {events.filter(ev=>ev.startDate===day && ev.resourceId===res.id).map(ev=>{
                      const top = minutesToY(ev.start);
                      const height = Math.max(24, minutesToY(ev.end)-minutesToY(ev.start));
                      return (
                        <div key={ev.id} className={`event-card position-absolute p-1`} style={{top, height, left:2,right:2, background:ev.color??"#6EE7B7", borderRadius:4, zIndex:selectedEventId===ev.id?30:20}}
                             onMouseDown={e=>onEventMouseDown(e,ev)}
                             onDoubleClick={()=>openEditor(ev)}>
                          <strong style={{fontSize:12,color:"#fff"}}>{ev.title}</strong>
                          <div style={{fontSize:10,color:"#fff"}}>{formatTime(ev.start)} - {formatTime(ev.end)}</div>
                          {/* Resize handle */}
                          <div style={{position:"absolute",bottom:0,right:0,left:0,height:4,cursor:"ns-resize", background:"#fff"}} onMouseDown={e=>onEventResizeMouseDown(e,ev,"bottom")}></div>
                        </div>
                      )
                    })}
                  </div>
                ))}
              </div>
            ))}
          </div>
        </div>
      </div>

      {/* Event Editor Modal */}
      {showEditor && (
        <div className="position-fixed top-0 start-0 w-100 h-100 d-flex justify-content-center align-items-center" style={{background:"rgba(0,0,0,0.6)", zIndex:200}}>
          <div className="card p-3" style={{minWidth:320}}>
            <h6>Edit Event</h6>
            <input className="form-control mb-2" value={editorData.title} onChange={e=>setEditorData(d=>({...d,title:e.target.value}))} />
            <input type="time" className="form-control mb-2" value={formatTime(editorData.start)} onChange={e=>{
              const [h,m]=e.target.value.split(":").map(Number);
              setEditorData(d=>({...d,start:h*60+m}));
            }} />
            <input type="time" className="form-control mb-2" value={formatTime(editorData.end)} onChange={e=>{
              const [h,m]=e.target.value.split(":").map(Number);
              setEditorData(d=>({...d,end:h*60+m}));
            }} />
            <select className="form-select mb-2" value={editorData.resourceId} onChange={e=>setEditorData(d=>({...d,resourceId:e.target.value}))}>
              {resources.map(r=><option key={r.id} value={r.id}>{r.name}</option>)}
            </select>
            <div className="d-flex justify-content-between">
              <button className="btn btn-sm btn-secondary" onClick={()=>setShowEditor(false)}>Cancel</button>
              <button className="btn btn-sm btn-primary" onClick={saveEditor}>Save</button>
            </div>
          </div>
        </div>
      )}
    </div>
  )
}
