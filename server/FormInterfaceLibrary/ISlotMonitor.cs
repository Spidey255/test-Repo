
namespace CPS.Proof.DFSExtension
{
    
    using System.Collections.Generic;
    

/// <summary>
/// Represents the interface that will be implemented by the SlotMonitor 
/// class.
/// </summary>
public interface ISlotMonitor
{
 /// <summary>
/// Represents the method used to validate a slotId
/// </summary>
/// <param name="token">
/// A <see cref="string"/> that holds the slot identifier
/// for the user.
/// </param>
/// <returns>
/// Returns true if slot is a valid slot. Otherwise false.
/// </returns>
bool IsSlotValid(ref SlotToken token, UserActivities activities);
}
}