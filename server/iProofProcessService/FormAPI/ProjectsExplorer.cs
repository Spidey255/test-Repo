  







namespace CPS.Proof.DFSExtension
{
	using System;
    using System.Collections.Generic;     
	using SRA.Proof.Infrastructure;
	using SRA.Proof.Helpers;
	using SRA.Proof.Middleware;
	using System.Linq;
	
	
	public partial class ProjectsExplorerProcessMetadata : ExtensionProcessBase
	{		

        public override long PackageId
        {
            get { return 20126; }
        }

        public override string PackageName
        {
            get { return "testGrid"; }
        }

		public override string PackageDescription
        {
            get { return "for testing purpose"; }
        }

        public override string ProcessId
        {
            get { return "B219A0EA-0254-4F69-B989-B681DD475183"; }
        }

        public override string ProcessName
        {
            get { return "ProjectsExplorer"; }
        }

		public override string ProcessDescription
        {
            get { return "Projects Explorer"; }
        }

        public override ProcessType ProcessType
        {
            get { return ProcessType.RuleBased; }
        }

      

		public override string FileGroupId
        {
            get { return "C8BA2E2C-CB74-4616-9139-B32125B1C0A1"; }
        } 

		public override string FileGroup
        {
            get { return "Default"; }
        } 
		
        public override string PackageProcessMapId
        {
            get {return "B219A0EA-0254-4F69-B989-B681DD475183";}
        }

		public override byte ProcessInstanceMode
        {
            get {return 3;}
        }		

		public override string MasterFormId
        {
            get {  
				   return "7816392B-A9EF-486D-88F9-AC7C972D679B";	}
        }
			
        public override string Comments => throw new NotImplementedException();
		
		
	}
}
 





namespace CPS.Proof.DFSExtension
{
	using System;
	using System.Collections.Concurrent;
    using System.Collections.Generic;      	
	using SRA.Proof.Helpers;
	using SRA.Proof.Infrastructure;
	using CPS.Proof.DFSExtension;
	

	public partial class ProjectsExplorerObjectFactory  : ExtObjectFactoryBase
    {		
       	

    	public override IExtBaseMetaData GetProcessInstance(string packageProcessMapId)
        {		  
             return (IExtBaseMetaData) new ProjectsExplorerProcessMetadata();            
        }

		
		public override Tuple<string, string> GetComboDataSource(string ElementName)
		{
			foreach (Triplet<string, string, string> cmbDS in ComboDataSource)
            {
				if(cmbDS.FirstValue==ElementName)
				{
					return new Tuple<string,string>(
							cmbDS.SecondValue,
							cmbDS.ThirdValue
						);
				}
			}
			return null;
		}
		
		private List<Tuple<string, int>> GridRowsPerPage = new List<Tuple< string, int>>
		{
		
	  			
			     new Tuple<string, int>("ProjectListActiveProjectspr",5),
			
						
			     new Tuple<string, int>("ProjectListActiveProjectspr",500),
			
						
		};

	   public override int GetGridRPP(string ElementName)
		{
			foreach (Tuple<string, int> grids in GridRowsPerPage)
            {
				if(grids.Item1==ElementName)
				{
					return Convert.ToInt32(grids.Item2);
				}
			}
			return 5;
		}

		public override Dictionary<string, string> GetQueryExpressionDataSource(string expressionId)
		{
			if(string.IsNullOrWhiteSpace(expressionId))
            {
			 return null;
			}

			Dictionary<string, string> data;

            data = new Dictionary<string, string>();
            foreach (Triplet<string, string, string> exp in queryExpressionData)
            {
                if (exp.FirstValue == expressionId)
                {
                    var unknown = new
                    {
                        name = exp.SecondValue, constring = exp.ThirdValue
                    };
                    data.Add(unknown.name, unknown.constring);
                }
            }

			return data;

		}

	


		private List<Triplet<string, string, string>> queryExpressionData = new List<Triplet<string, string, string>>
		{
	  			
			     new Triplet<string, string, string>("DE7234F0-9899-46C5-8758-427FAF6AEB5A","C3BEA3AF-C9B7-4DEA-AE35-EA1C626191C0",
												     @"062C4298-5FB2-4C83-877D-A1D5795686CF"),
			
						
			     new Triplet<string, string, string>("B83852E3-6E4B-4A3A-8E3F-2F8AA8E3419F","C3BEA3AF-C9B7-4DEA-AE35-EA1C626191C0",
												     @"062C4298-5FB2-4C83-877D-A1D5795686CF"),
			
						
			     new Triplet<string, string, string>("BA80C2CD-020D-491F-8055-BD63F5ADE3A5","C3BEA3AF-C9B7-4DEA-AE35-EA1C626191C0",
												     @"062C4298-5FB2-4C83-877D-A1D5795686CF"),
			
						
			     new Triplet<string, string, string>("4A8693DC-3F25-4BCE-9F85-A49D2A429EE1","C3BEA3AF-C9B7-4DEA-AE35-EA1C626191C0",
												     @"062C4298-5FB2-4C83-877D-A1D5795686CF"),
			
						
			     new Triplet<string, string, string>("DE7234F0-9899-46C5-8758-427FAF6AEB5A","Grid Binding",
												     @"BDF7896C-4676-4008-BC64-2C45896293B5"),
			
			
			
		};

		
		private List<Triplet<string, string, string>> ComboDataSource = new List<Triplet<string, string, string>>
		{

		
	  			
			     new Triplet<string, string, string>("MF_ProjectListActiveProjectspr_GridRows","Z4IrlPTfl7E3UHhZKuDmi6JCY+YHcA863SqFhgUyVVzIeVWsNWiw11TGEbWcCHGEL9UeHd58FPuIqzlLTknSORJ8vwVq8b6q3+xK7qoaYryCecExdgZwESRUvAQUkQSXyP0ob1BIgO9asLeZ3YsYF6QtOlQpT2BA",
												     @"EXEC [dbo].[SP_GetAutoFormGridRows]"),
			
						
		};
		
	


		public override IVirtualPage GetDfsVirtualInstance(string processActivityMapId)   
		{
			IVirtualPage virtualInstnace = GetVirtualInstnace(processActivityMapId);				

			return virtualInstnace;
		} 

	


		private IVirtualPage GetVirtualInstnace(string processActivityMapId)
		{
				

			
			
			IVirtualPage virtualInstance = null;
			

			switch(processActivityMapId)	
				{
											case "34CF8FAF-7574-478F-9C4A-A65F98E4D18A":
							virtualInstance=new ISpace34CF8FAF7574478F9C4AA65F98E4D18A();
							break;
					
					
					default:
						break;
				}
			
			
						

			return virtualInstance;	
		}

	}
}	
namespace CPS.Proof.DFSExtension
{

using System.Collections.Generic;
using System;
using SRA.Proof.Helpers;
using CPS.Proof.DFSExtension;
using SRA.Proof.Infrastructure;
using System.Linq;using System.Data;
using System.Runtime.CompilerServices;
public class ISpace34CF8FAF7574478F9C4AA65F98E4D18A : VirtualForm
{
IISpace iSpace = new ISpace();
AcDataISpace34CF8FAF7574478F9C4AA65F98E4D18A acdataIspace34CF8FAF7574478F9C4AA65F98E4D18A=new AcDataISpace34CF8FAF7574478F9C4AA65F98E4D18A();
private void SubscribeFormEvents_root(ref Dictionary<string, ServiceElementData> ISpace)
{
try
{
Dictionary<short, object> keyValuePairs = null;base.WriteDebugInfo(@"Root-OnAfterFormLoad");
if(1==1)
{
base.WriteDebugInfo(@"if(1==1)");
if(ISpace["FormVersionId"].Value=="02A1FE25-AE94-461D-9B6F-A7AC8493B6F6")
{
ISpace["UI_GridPanel"].Visible="true";ISpace["UI_GridPanel"].Enbl="true";
}
base.WriteDebugInfo(@"if(ISpace[""FormVersionId""].Value==""02A1FE25-AE94-461D-9B6F-A7AC8493B6F6"")
{
ISpace[""UI_GridPanel""].Visible=""true"";ISpace[""UI_GridPanel""].Enbl=""true"";
}");
}
if(1==1)
{
base.WriteDebugInfo(@"if(1==1)");

var querySourceDE7234F0989946C58758427FAF6AEB5A =GetQueryExpressionDataSource("DE7234F0-9899-46C5-8758-427FAF6AEB5A");

DataTable resultDE7234F0989946C58758427FAF6AEB5A=iSpace.SetGridDataSource(querySourceDE7234F0989946C58758427FAF6AEB5A,@"EXEC dbo.[GetProjectExplorerDetails] 1,'" + ISpace["F_ProjectDetailsId"].Value + @"','','','" + ISpace["MF_ProjectListActiveProjectspr_GridRows"].Value + @"',''");


base.WriteDebugInfo(@"var querySourceDE7234F0989946C58758427FAF6AEB5A =GetQueryExpressionDataSource(""DE7234F0-9899-46C5-8758-427FAF6AEB5A"");DataTable resultDE7234F0989946C58758427FAF6AEB5A=iSpace.SetGridDataSource(querySourceDE7234F0989946C58758427FAF6AEB5A,@""EXEC dbo.[GetProjectExplorerDetails] 1,'"" + ISpace[""F_ProjectDetailsId""].Value + @""','','','"" + ISpace[""MF_ProjectListActiveProjectspr_GridRows""].Value + @""',''"");");
base.WriteDebugInfo(@"EXEC dbo.[Get_bccb6bb6-39f0-4863-8a17-2efdffe2a45b_FilterData] 1,'#F_ProjectDetailsId','',#MF_ProjectListActiveProjectspr_PageIndex,'#MF_ProjectListActiveProjectspr_GridRows'");


List<Triplet<string, short, short?>> result28E58773E71A46B18591A5E00111B3CB=acdataIspace34CF8FAF7574478F9C4AA65F98E4D18A.GetQueryExpressionBindings("28E58773-E71A-46B1-8591-A5E00111B3CB");
iSpace.SetGridData(resultDE7234F0989946C58758427FAF6AEB5A,result28E58773E71A46B18591A5E00111B3CB,"ProjectListActiveProjectspr",ref ISpace);
}
if(1==1)
{
base.WriteDebugInfo(@"if(1==1)");
base.WriteDebugInfo(@"Exec GetProjectStatistics ' '");

var querySourceBA80C2CD020D491F8055BD63F5ADE3A5 =GetQueryExpressionDataSource("BA80C2CD-020D-491F-8055-BD63F5ADE3A5");
Dictionary<short,object> resultBA80C2CD020D491F8055BD63F5ADE3A5=iSpace.ExecuteQuery(querySourceBA80C2CD020D491F8055BD63F5ADE3A5,@"ExecGetProjectStatistics ' '",false);

base.WriteDebugInfo(@"var querySourceBA80C2CD020D491F8055BD63F5ADE3A5 =GetQueryExpressionDataSource(""BA80C2CD-020D-491F-8055-BD63F5ADE3A5"");Dictionary<short,object> resultBA80C2CD020D491F8055BD63F5ADE3A5=iSpace.ExecuteQuery(querySourceBA80C2CD020D491F8055BD63F5ADE3A5,@""ExecGetProjectStatistics ' '"",false);");
base.WriteDebugInfo(@"");

if((resultBA80C2CD020D491F8055BD63F5ADE3A5!=null) && (resultBA80C2CD020D491F8055BD63F5ADE3A5.Count!=0))
{
if(resultBA80C2CD020D491F8055BD63F5ADE3A5.ContainsKey(7))
ISpace["MF_SanctionedAmount"].Value = resultBA80C2CD020D491F8055BD63F5ADE3A5[7];
if(resultBA80C2CD020D491F8055BD63F5ADE3A5.ContainsKey(6))
ISpace["MF_SanctionedCount"].Value = resultBA80C2CD020D491F8055BD63F5ADE3A5[6];
if(resultBA80C2CD020D491F8055BD63F5ADE3A5.ContainsKey(5))
ISpace["MF_ProposalAmount"].Value = resultBA80C2CD020D491F8055BD63F5ADE3A5[5];
if(resultBA80C2CD020D491F8055BD63F5ADE3A5.ContainsKey(0))
ISpace["MF_ActiveCount"].Value = resultBA80C2CD020D491F8055BD63F5ADE3A5[0];
if(resultBA80C2CD020D491F8055BD63F5ADE3A5.ContainsKey(3))
ISpace["MF_CompletedAmount"].Value = resultBA80C2CD020D491F8055BD63F5ADE3A5[3];
if(resultBA80C2CD020D491F8055BD63F5ADE3A5.ContainsKey(1))
ISpace["MF_ActiveAmount"].Value = resultBA80C2CD020D491F8055BD63F5ADE3A5[1];
if(resultBA80C2CD020D491F8055BD63F5ADE3A5.ContainsKey(2))
ISpace["MF_CompletedCount"].Value = resultBA80C2CD020D491F8055BD63F5ADE3A5[2];
if(resultBA80C2CD020D491F8055BD63F5ADE3A5.ContainsKey(4))
ISpace["MF_ProposalCount"].Value = resultBA80C2CD020D491F8055BD63F5ADE3A5[4];
}
else{
ISpace["MF_SanctionedAmount"].Value = null;ISpace["MF_SanctionedCount"].Value = null;ISpace["MF_ProposalAmount"].Value = null;ISpace["MF_ActiveCount"].Value = null;ISpace["MF_CompletedAmount"].Value = null;ISpace["MF_ActiveAmount"].Value = null;ISpace["MF_CompletedCount"].Value = null;ISpace["MF_ProposalCount"].Value = null;
}
}
}
catch(Exception ex)
{
base.WriteErrorInfo(@"Exception:",ex);
}
}
private void SubscribeElementEvents_add (ref Dictionary<string,ServiceElementData> ISpace)
{
IISpace iSpace = new ISpace();
try
{
base.WriteDebugInfo(@"Add-OnClick");
if(1==1)
{
base.WriteDebugInfo(@"if(1==1)");
if(ISpace["FormVersionId"].Value=="02A1FE25-AE94-461D-9B6F-A7AC8493B6F6")
{
ISpace["UI_GridPanel"].Visible="false";ISpace["UI_GridPanel"].Enbl="true";
}
base.WriteDebugInfo(@"if(ISpace[""FormVersionId""].Value==""02A1FE25-AE94-461D-9B6F-A7AC8493B6F6"")
{
ISpace[""UI_GridPanel""].Visible=""false"";ISpace[""UI_GridPanel""].Enbl=""true"";
}");
ISpace["MF_V1_ProjectDetailsID"].Value=null;
base.WriteDebugInfo(@"ISpace[""MF_V1_ProjectDetailsID""].Value=null;");
ISpace["MF_V1_ProjectTitle"].Value=null;
base.WriteDebugInfo(@"ISpace[""MF_V1_ProjectTitle""].Value=null;");
ISpace["MF_V1_ProjectNo"].Value=null;
base.WriteDebugInfo(@"ISpace[""MF_V1_ProjectNo""].Value=null;");
ISpace["MF_V1_Department"].Value=null;
base.WriteDebugInfo(@"ISpace[""MF_V1_Department""].Value=null;");
ISpace["MF_V1_PI"].Value=null;
base.WriteDebugInfo(@"ISpace[""MF_V1_PI""].Value=null;");
ISpace["MF_V1_CoPI"].Value=null;
base.WriteDebugInfo(@"ISpace[""MF_V1_CoPI""].Value=null;");
ISpace["MF_V1_ProjectType"].Value=null;
base.WriteDebugInfo(@"ISpace[""MF_V1_ProjectType""].Value=null;");
ISpace["MF_V1_Agency"].Value=null;
base.WriteDebugInfo(@"ISpace[""MF_V1_Agency""].Value=null;");
ISpace["MF_V1_StartDate"].Value=null;
base.WriteDebugInfo(@"ISpace[""MF_V1_StartDate""].Value=null;");
ISpace["MF_V1_EndDate"].Value=null;
base.WriteDebugInfo(@"ISpace[""MF_V1_EndDate""].Value=null;");
ISpace["MF_V1_Duration"].Value=null;
base.WriteDebugInfo(@"ISpace[""MF_V1_Duration""].Value=null;");
ISpace["MF_V1_FinancialYear"].Value=null;
base.WriteDebugInfo(@"ISpace[""MF_V1_FinancialYear""].Value=null;");
ISpace["MF_V1_SanctionedValue"].Value=null;
base.WriteDebugInfo(@"ISpace[""MF_V1_SanctionedValue""].Value=null;");
}
}
catch(Exception ex)
{
base.WriteErrorInfo(@"Exception:",ex);
}
}
private void SubscribeElementEvents_mf_projectlistactiveprojectspr_pageindex (ref Dictionary<string,ServiceElementData> ISpace)
{
IISpace iSpace = new ISpace();
try
{
base.WriteDebugInfo(@"MF_ProjectListActiveProjectspr_PageIndex-OnChange");
if(1==1)
{
base.WriteDebugInfo(@"if(1==1)");

var querySourceDE7234F0989946C58758427FAF6AEB5A =GetQueryExpressionDataSource("DE7234F0-9899-46C5-8758-427FAF6AEB5A");

DataTable resultDE7234F0989946C58758427FAF6AEB5A=iSpace.SetGridDataSource(querySourceDE7234F0989946C58758427FAF6AEB5A,@"EXEC dbo.[GetProjectExplorerDetails] 1,'" + ISpace["F_ProjectDetailsId"].Value + @"','','','" + ISpace["MF_ProjectListActiveProjectspr_GridRows"].Value + @"',''");


base.WriteDebugInfo(@"var querySourceDE7234F0989946C58758427FAF6AEB5A =GetQueryExpressionDataSource(""DE7234F0-9899-46C5-8758-427FAF6AEB5A"");DataTable resultDE7234F0989946C58758427FAF6AEB5A=iSpace.SetGridDataSource(querySourceDE7234F0989946C58758427FAF6AEB5A,@""EXEC dbo.[GetProjectExplorerDetails] 1,'"" + ISpace[""F_ProjectDetailsId""].Value + @""','','','"" + ISpace[""MF_ProjectListActiveProjectspr_GridRows""].Value + @""',''"");");
base.WriteDebugInfo(@"EXEC dbo.[Get_bccb6bb6-39f0-4863-8a17-2efdffe2a45b_FilterData] 1,'#F_ProjectDetailsId','',#MF_ProjectListActiveProjectspr_PageIndex,'#MF_ProjectListActiveProjectspr_GridRows'");


List<Triplet<string, short, short?>> result28E58773E71A46B18591A5E00111B3CB=acdataIspace34CF8FAF7574478F9C4AA65F98E4D18A.GetQueryExpressionBindings("28E58773-E71A-46B1-8591-A5E00111B3CB");
iSpace.SetGridData(resultDE7234F0989946C58758427FAF6AEB5A,result28E58773E71A46B18591A5E00111B3CB,"ProjectListActiveProjectspr",ref ISpace);
}
}
catch(Exception ex)
{
base.WriteErrorInfo(@"Exception:",ex);
}
}
private void SubscribeElementEvents_mgg_v1_projecttitle (ref Dictionary<string,ServiceElementData> ISpace)
{
IISpace iSpace = new ISpace();
try
{
base.WriteDebugInfo(@"MGG_V1_ProjectTitle-OnClick");
ISpace["MF_URL"].Value="http://210.18.135.72/frameportal/#/ProjectView/3/"+ISpace["MGG_V1_ProjectDetailsID"].Value;
base.WriteDebugInfo(@"ISpace[""MF_URL""].Value=""http://210.18.135.72/frameportal/#/ProjectView/3/""+ISpace[""MGG_V1_ProjectDetailsID""].Value;");
if(1==1)
{
base.WriteDebugInfo(@"if(1==1)");

ISpace["2DB6B3E7-2CC1-433F-BFF7-B8C6B68A7A0B"].RedirectType=1;
ISpace["2DB6B3E7-2CC1-433F-BFF7-B8C6B68A7A0B"].Value=ISpace["MF_URL"].Value;
base.WriteDebugInfo(@"
ISpace[""2DB6B3E7-2CC1-433F-BFF7-B8C6B68A7A0B""].RedirectType=1;
ISpace[""2DB6B3E7-2CC1-433F-BFF7-B8C6B68A7A0B""].Value=ISpace[""MF_URL""].Value;");
}
}
catch(Exception ex)
{
base.WriteErrorInfo(@"Exception:",ex);
}
}
private void SubscribeElementEvents_save (ref Dictionary<string,ServiceElementData> ISpace)
{
IISpace iSpace = new ISpace();
try
{
base.WriteDebugInfo(@"Save-OnClick");
if(1==1)
{
base.WriteDebugInfo(@"if(1==1)");
base.WriteDebugInfo(@"EXEC dbo.[Dsp_6D679FD7-C357-433A-81A5-3FC3AD3E9AAA_Save] '#MF_V1_ProjectDetailsID', '#MF_V1_ProjectTitle', '#MF_V1_ProjectNo', '#MF_V1_Department', '#MF_V1_PI', '#MF_V1_CoPI', '#MF_V1_ProjectType', '#MF_V1_Agency', '#MF_V1_StartDate', '#MF_V1_EndDate', '#MF_V1_Duration', '#MF_V1_FinancialYear', '#MF_V1_SanctionedValue'");

var querySource4A8693DC3F254BCE9F85A49D2A429EE1 =GetQueryExpressionDataSource("4A8693DC-3F25-4BCE-9F85-A49D2A429EE1");
Dictionary<short,object> result4A8693DC3F254BCE9F85A49D2A429EE1=iSpace.ExecuteQuery(querySource4A8693DC3F254BCE9F85A49D2A429EE1,@"EXEC dbo.[Dsp_6D679FD7-C357-433A-81A5-3FC3AD3E9AAA_Save] '" + ISpace["MF_V1_ProjectDetailsID"].Value + @"', '" + ISpace["MF_V1_ProjectTitle"].Value + @"', '" + ISpace["MF_V1_ProjectNo"].Value + @"', '" + ISpace["MF_V1_Department"].Value + @"', '" + ISpace["MF_V1_PI"].Value + @"', '" + ISpace["MF_V1_CoPI"].Value + @"', '" + ISpace["MF_V1_ProjectType"].Value + @"', '" + ISpace["MF_V1_Agency"].Value + @"', '" + ISpace["MF_V1_StartDate"].Value + @"', '" + ISpace["MF_V1_EndDate"].Value + @"', '" + ISpace["MF_V1_Duration"].Value + @"', '" + ISpace["MF_V1_FinancialYear"].Value + @"', '" + ISpace["MF_V1_SanctionedValue"].Value + @"'",false);

base.WriteDebugInfo(@"var querySource4A8693DC3F254BCE9F85A49D2A429EE1 =GetQueryExpressionDataSource(""4A8693DC-3F25-4BCE-9F85-A49D2A429EE1"");Dictionary<short,object> result4A8693DC3F254BCE9F85A49D2A429EE1=iSpace.ExecuteQuery(querySource4A8693DC3F254BCE9F85A49D2A429EE1,@""EXEC dbo.[Dsp_6D679FD7-C357-433A-81A5-3FC3AD3E9AAA_Save] '"" + ISpace[""MF_V1_ProjectDetailsID""].Value + @""', '"" + ISpace[""MF_V1_ProjectTitle""].Value + @""', '"" + ISpace[""MF_V1_ProjectNo""].Value + @""', '"" + ISpace[""MF_V1_Department""].Value + @""', '"" + ISpace[""MF_V1_PI""].Value + @""', '"" + ISpace[""MF_V1_CoPI""].Value + @""', '"" + ISpace[""MF_V1_ProjectType""].Value + @""', '"" + ISpace[""MF_V1_Agency""].Value + @""', '"" + ISpace[""MF_V1_StartDate""].Value + @""', '"" + ISpace[""MF_V1_EndDate""].Value + @""', '"" + ISpace[""MF_V1_Duration""].Value + @""', '"" + ISpace[""MF_V1_FinancialYear""].Value + @""', '"" + ISpace[""MF_V1_SanctionedValue""].Value + @""'"",false);");
base.WriteDebugInfo(@"EXEC dbo.[Dsp_6D679FD7-C357-433A-81A5-3FC3AD3E9AAA_Save] '#MF_V1_ProjectDetailsID', '#MF_V1_ProjectTitle', '#MF_V1_ProjectNo', '#MF_V1_Department', '#MF_V1_PI', '#MF_V1_CoPI', '#MF_V1_ProjectType', '#MF_V1_Agency', '#MF_V1_StartDate', '#MF_V1_EndDate', '#MF_V1_Duration', '#MF_V1_FinancialYear', '#MF_V1_SanctionedValue'");

if((result4A8693DC3F254BCE9F85A49D2A429EE1!=null) && (result4A8693DC3F254BCE9F85A49D2A429EE1.Count!=0))
{
if(result4A8693DC3F254BCE9F85A49D2A429EE1.ContainsKey(0))
ISpace["Subject"].Value = result4A8693DC3F254BCE9F85A49D2A429EE1[0];
}
else{
ISpace["Subject"].Value = null;
}
}
if(1==1)
{
base.WriteDebugInfo(@"if(1==1)");
if(ISpace["FormVersionId"].Value=="02A1FE25-AE94-461D-9B6F-A7AC8493B6F6")
{
ISpace["UI_GridPanel"].Visible="true";ISpace["UI_GridPanel"].Enbl="true";
}
base.WriteDebugInfo(@"if(ISpace[""FormVersionId""].Value==""02A1FE25-AE94-461D-9B6F-A7AC8493B6F6"")
{
ISpace[""UI_GridPanel""].Visible=""true"";ISpace[""UI_GridPanel""].Enbl=""true"";
}");
}
if(1==1)
{
base.WriteDebugInfo(@"if(1==1)");

var querySourceDE7234F0989946C58758427FAF6AEB5A =GetQueryExpressionDataSource("DE7234F0-9899-46C5-8758-427FAF6AEB5A");

DataTable resultDE7234F0989946C58758427FAF6AEB5A=iSpace.SetGridDataSource(querySourceDE7234F0989946C58758427FAF6AEB5A,@"EXEC dbo.[GetProjectExplorerDetails] 1,'" + ISpace["F_ProjectDetailsId"].Value + @"','','','" + ISpace["MF_ProjectListActiveProjectspr_GridRows"].Value + @"',''");


base.WriteDebugInfo(@"var querySourceDE7234F0989946C58758427FAF6AEB5A =GetQueryExpressionDataSource(""DE7234F0-9899-46C5-8758-427FAF6AEB5A"");DataTable resultDE7234F0989946C58758427FAF6AEB5A=iSpace.SetGridDataSource(querySourceDE7234F0989946C58758427FAF6AEB5A,@""EXEC dbo.[GetProjectExplorerDetails] 1,'"" + ISpace[""F_ProjectDetailsId""].Value + @""','','','"" + ISpace[""MF_ProjectListActiveProjectspr_GridRows""].Value + @""',''"");");
base.WriteDebugInfo(@"EXEC dbo.[Get_bccb6bb6-39f0-4863-8a17-2efdffe2a45b_FilterData] 1,'#F_ProjectDetailsId','',#MF_ProjectListActiveProjectspr_PageIndex,'#MF_ProjectListActiveProjectspr_GridRows'");


List<Triplet<string, short, short?>> result28E58773E71A46B18591A5E00111B3CB=acdataIspace34CF8FAF7574478F9C4AA65F98E4D18A.GetQueryExpressionBindings("28E58773-E71A-46B1-8591-A5E00111B3CB");
iSpace.SetGridData(resultDE7234F0989946C58758427FAF6AEB5A,result28E58773E71A46B18591A5E00111B3CB,"ProjectListActiveProjectspr",ref ISpace);
}
}
catch(Exception ex)
{
base.WriteErrorInfo(@"Exception:",ex);
}
}
private void SubscribeElementEvents_edit (ref Dictionary<string,ServiceElementData> ISpace)
{
IISpace iSpace = new ISpace();
try
{
base.WriteDebugInfo(@"Edit-OnClick");
if(1==1)
{
base.WriteDebugInfo(@"if(1==1)");
if(ISpace["FormVersionId"].Value=="02A1FE25-AE94-461D-9B6F-A7AC8493B6F6")
{
ISpace["UI_GridPanel"].Visible="false";ISpace["UI_GridPanel"].Enbl="true";
}
base.WriteDebugInfo(@"if(ISpace[""FormVersionId""].Value==""02A1FE25-AE94-461D-9B6F-A7AC8493B6F6"")
{
ISpace[""UI_GridPanel""].Visible=""false"";ISpace[""UI_GridPanel""].Enbl=""true"";
}");
ISpace["MF_V1_ProjectDetailsID"].Value=null;
base.WriteDebugInfo(@"ISpace[""MF_V1_ProjectDetailsID""].Value=null;");
ISpace["MF_V1_ProjectTitle"].Value=null;
base.WriteDebugInfo(@"ISpace[""MF_V1_ProjectTitle""].Value=null;");
ISpace["MF_V1_ProjectNo"].Value=null;
base.WriteDebugInfo(@"ISpace[""MF_V1_ProjectNo""].Value=null;");
ISpace["MF_V1_Department"].Value=null;
base.WriteDebugInfo(@"ISpace[""MF_V1_Department""].Value=null;");
ISpace["MF_V1_PI"].Value=null;
base.WriteDebugInfo(@"ISpace[""MF_V1_PI""].Value=null;");
ISpace["MF_V1_CoPI"].Value=null;
base.WriteDebugInfo(@"ISpace[""MF_V1_CoPI""].Value=null;");
ISpace["MF_V1_ProjectType"].Value=null;
base.WriteDebugInfo(@"ISpace[""MF_V1_ProjectType""].Value=null;");
ISpace["MF_V1_Agency"].Value=null;
base.WriteDebugInfo(@"ISpace[""MF_V1_Agency""].Value=null;");
ISpace["MF_V1_StartDate"].Value=null;
base.WriteDebugInfo(@"ISpace[""MF_V1_StartDate""].Value=null;");
ISpace["MF_V1_EndDate"].Value=null;
base.WriteDebugInfo(@"ISpace[""MF_V1_EndDate""].Value=null;");
ISpace["MF_V1_Duration"].Value=null;
base.WriteDebugInfo(@"ISpace[""MF_V1_Duration""].Value=null;");
ISpace["MF_V1_FinancialYear"].Value=null;
base.WriteDebugInfo(@"ISpace[""MF_V1_FinancialYear""].Value=null;");
ISpace["MF_V1_SanctionedValue"].Value=null;
base.WriteDebugInfo(@"ISpace[""MF_V1_SanctionedValue""].Value=null;");
}
if(1==1)
{
base.WriteDebugInfo(@"if(1==1)");
base.WriteDebugInfo(@"SELECT VW_MyProjects_1.[ProjectDetailsID], VW_MyProjects_1.[ProjectTitle], VW_MyProjects_1.[ProjectNo], VW_MyProjects_1.[Department], VW_MyProjects_1.[PI], VW_MyProjects_1.[CoPI], VW_MyProjects_1.[ProjectType], VW_MyProjects_1.[Agency], VW_MyProjects_1.[StartDate], VW_MyProjects_1.[EndDate], VW_MyProjects_1.[Duration], VW_MyProjects_1.[FinancialYear], VW_MyProjects_1.[SanctionedValue] FROM  Projects..VW_MyProjects_1  WHERE VW_MyProjects_1.ProjectDetailsID= '#MGG_V1_ProjectDetailsID'");

var querySourceB83852E36E4B4A3A8E3F2F8AA8E3419F =GetQueryExpressionDataSource("B83852E3-6E4B-4A3A-8E3F-2F8AA8E3419F");
Dictionary<short,object> resultB83852E36E4B4A3A8E3F2F8AA8E3419F=iSpace.ExecuteQuery(querySourceB83852E36E4B4A3A8E3F2F8AA8E3419F,@"SELECT VW_MyProjects_1.[ProjectDetailsID], VW_MyProjects_1.[ProjectTitle], VW_MyProjects_1.[ProjectNo], VW_MyProjects_1.[Department], VW_MyProjects_1.[PI], VW_MyProjects_1.[CoPI], VW_MyProjects_1.[ProjectType], VW_MyProjects_1.[Agency], VW_MyProjects_1.[StartDate], VW_MyProjects_1.[EndDate], VW_MyProjects_1.[Duration], VW_MyProjects_1.[FinancialYear], VW_MyProjects_1.[SanctionedValue] FROM  Projects..VW_MyProjects_1  WHERE VW_MyProjects_1.ProjectDetailsID= '" + ISpace["MGG_V1_ProjectDetailsID"].Value + @"'",false);

base.WriteDebugInfo(@"var querySourceB83852E36E4B4A3A8E3F2F8AA8E3419F =GetQueryExpressionDataSource(""B83852E3-6E4B-4A3A-8E3F-2F8AA8E3419F"");Dictionary<short,object> resultB83852E36E4B4A3A8E3F2F8AA8E3419F=iSpace.ExecuteQuery(querySourceB83852E36E4B4A3A8E3F2F8AA8E3419F,@""SELECT VW_MyProjects_1.[ProjectDetailsID], VW_MyProjects_1.[ProjectTitle], VW_MyProjects_1.[ProjectNo], VW_MyProjects_1.[Department], VW_MyProjects_1.[PI], VW_MyProjects_1.[CoPI], VW_MyProjects_1.[ProjectType], VW_MyProjects_1.[Agency], VW_MyProjects_1.[StartDate], VW_MyProjects_1.[EndDate], VW_MyProjects_1.[Duration], VW_MyProjects_1.[FinancialYear], VW_MyProjects_1.[SanctionedValue] FROM  Projects..VW_MyProjects_1  WHERE VW_MyProjects_1.ProjectDetailsID= '"" + ISpace[""MGG_V1_ProjectDetailsID""].Value + @""'"",false);");
base.WriteDebugInfo(@"SELECT VW_MyProjects_1.[ProjectDetailsID], VW_MyProjects_1.[ProjectTitle], VW_MyProjects_1.[ProjectNo], VW_MyProjects_1.[Department], VW_MyProjects_1.[PI], VW_MyProjects_1.[CoPI], VW_MyProjects_1.[ProjectType], VW_MyProjects_1.[Agency], VW_MyProjects_1.[StartDate], VW_MyProjects_1.[EndDate], VW_MyProjects_1.[Duration], VW_MyProjects_1.[FinancialYear], VW_MyProjects_1.[SanctionedValue] FROM  Projects..VW_MyProjects_1  WHERE VW_MyProjects_1.ProjectDetailsID= '#MGG_V1_ProjectDetailsID'");

if((resultB83852E36E4B4A3A8E3F2F8AA8E3419F!=null) && (resultB83852E36E4B4A3A8E3F2F8AA8E3419F.Count!=0))
{
if(resultB83852E36E4B4A3A8E3F2F8AA8E3419F.ContainsKey(8))
ISpace["MF_V1_StartDate"].Value = resultB83852E36E4B4A3A8E3F2F8AA8E3419F[8];
if(resultB83852E36E4B4A3A8E3F2F8AA8E3419F.ContainsKey(9))
ISpace["MF_V1_EndDate"].Value = resultB83852E36E4B4A3A8E3F2F8AA8E3419F[9];
if(resultB83852E36E4B4A3A8E3F2F8AA8E3419F.ContainsKey(12))
ISpace["MF_V1_SanctionedValue"].Value = resultB83852E36E4B4A3A8E3F2F8AA8E3419F[12];
if(resultB83852E36E4B4A3A8E3F2F8AA8E3419F.ContainsKey(11))
ISpace["MF_V1_FinancialYear"].Value = resultB83852E36E4B4A3A8E3F2F8AA8E3419F[11];
if(resultB83852E36E4B4A3A8E3F2F8AA8E3419F.ContainsKey(6))
ISpace["MF_V1_ProjectType"].Value = resultB83852E36E4B4A3A8E3F2F8AA8E3419F[6];
if(resultB83852E36E4B4A3A8E3F2F8AA8E3419F.ContainsKey(0))
ISpace["MF_V1_ProjectDetailsID"].Value = resultB83852E36E4B4A3A8E3F2F8AA8E3419F[0];
if(resultB83852E36E4B4A3A8E3F2F8AA8E3419F.ContainsKey(1))
ISpace["MF_V1_ProjectTitle"].Value = resultB83852E36E4B4A3A8E3F2F8AA8E3419F[1];
if(resultB83852E36E4B4A3A8E3F2F8AA8E3419F.ContainsKey(7))
ISpace["MF_V1_Agency"].Value = resultB83852E36E4B4A3A8E3F2F8AA8E3419F[7];
if(resultB83852E36E4B4A3A8E3F2F8AA8E3419F.ContainsKey(10))
ISpace["MF_V1_Duration"].Value = resultB83852E36E4B4A3A8E3F2F8AA8E3419F[10];
if(resultB83852E36E4B4A3A8E3F2F8AA8E3419F.ContainsKey(4))
ISpace["MF_V1_PI"].Value = resultB83852E36E4B4A3A8E3F2F8AA8E3419F[4];
if(resultB83852E36E4B4A3A8E3F2F8AA8E3419F.ContainsKey(5))
ISpace["MF_V1_CoPI"].Value = resultB83852E36E4B4A3A8E3F2F8AA8E3419F[5];
if(resultB83852E36E4B4A3A8E3F2F8AA8E3419F.ContainsKey(2))
ISpace["MF_V1_ProjectNo"].Value = resultB83852E36E4B4A3A8E3F2F8AA8E3419F[2];
if(resultB83852E36E4B4A3A8E3F2F8AA8E3419F.ContainsKey(3))
ISpace["MF_V1_Department"].Value = resultB83852E36E4B4A3A8E3F2F8AA8E3419F[3];
}
else{
ISpace["MF_V1_StartDate"].Value = null;ISpace["MF_V1_EndDate"].Value = null;ISpace["MF_V1_SanctionedValue"].Value = null;ISpace["MF_V1_FinancialYear"].Value = null;ISpace["MF_V1_ProjectType"].Value = null;ISpace["MF_V1_ProjectDetailsID"].Value = null;ISpace["MF_V1_ProjectTitle"].Value = null;ISpace["MF_V1_Agency"].Value = null;ISpace["MF_V1_Duration"].Value = null;ISpace["MF_V1_PI"].Value = null;ISpace["MF_V1_CoPI"].Value = null;ISpace["MF_V1_ProjectNo"].Value = null;ISpace["MF_V1_Department"].Value = null;
}
}
}
catch(Exception ex)
{
base.WriteErrorInfo(@"Exception:",ex);
}
}
private void SubscribeElementEvents_apply (ref Dictionary<string,ServiceElementData> ISpace)
{
IISpace iSpace = new ISpace();
try
{
base.WriteDebugInfo(@"Apply-OnClick");
if(1==1)
{
base.WriteDebugInfo(@"if(1==1)");

var querySourceDE7234F0989946C58758427FAF6AEB5A =GetQueryExpressionDataSource("DE7234F0-9899-46C5-8758-427FAF6AEB5A");

DataTable resultDE7234F0989946C58758427FAF6AEB5A=iSpace.SetGridDataSource(querySourceDE7234F0989946C58758427FAF6AEB5A,@"EXEC dbo.[GetProjectExplorerDetails] 1,'" + ISpace["F_ProjectDetailsId"].Value + @"','','','" + ISpace["MF_ProjectListActiveProjectspr_GridRows"].Value + @"',''");


base.WriteDebugInfo(@"var querySourceDE7234F0989946C58758427FAF6AEB5A =GetQueryExpressionDataSource(""DE7234F0-9899-46C5-8758-427FAF6AEB5A"");DataTable resultDE7234F0989946C58758427FAF6AEB5A=iSpace.SetGridDataSource(querySourceDE7234F0989946C58758427FAF6AEB5A,@""EXEC dbo.[GetProjectExplorerDetails] 1,'"" + ISpace[""F_ProjectDetailsId""].Value + @""','','','"" + ISpace[""MF_ProjectListActiveProjectspr_GridRows""].Value + @""',''"");");
base.WriteDebugInfo(@"EXEC dbo.[Get_bccb6bb6-39f0-4863-8a17-2efdffe2a45b_FilterData] 1,'#F_ProjectDetailsId','',#MF_ProjectListActiveProjectspr_PageIndex,'#MF_ProjectListActiveProjectspr_GridRows'");


List<Triplet<string, short, short?>> result28E58773E71A46B18591A5E00111B3CB=acdataIspace34CF8FAF7574478F9C4AA65F98E4D18A.GetQueryExpressionBindings("28E58773-E71A-46B1-8591-A5E00111B3CB");
iSpace.SetGridData(resultDE7234F0989946C58758427FAF6AEB5A,result28E58773E71A46B18591A5E00111B3CB,"ProjectListActiveProjectspr",ref ISpace);
}
if(1==1)
{
base.WriteDebugInfo(@"if(1==1)");
base.WriteDebugInfo(@"Exec GetProjectStatistics ' '");

var querySourceBA80C2CD020D491F8055BD63F5ADE3A5 =GetQueryExpressionDataSource("BA80C2CD-020D-491F-8055-BD63F5ADE3A5");
Dictionary<short,object> resultBA80C2CD020D491F8055BD63F5ADE3A5=iSpace.ExecuteQuery(querySourceBA80C2CD020D491F8055BD63F5ADE3A5,@"ExecGetProjectStatistics ' '",false);

base.WriteDebugInfo(@"var querySourceBA80C2CD020D491F8055BD63F5ADE3A5 =GetQueryExpressionDataSource(""BA80C2CD-020D-491F-8055-BD63F5ADE3A5"");Dictionary<short,object> resultBA80C2CD020D491F8055BD63F5ADE3A5=iSpace.ExecuteQuery(querySourceBA80C2CD020D491F8055BD63F5ADE3A5,@""ExecGetProjectStatistics ' '"",false);");
base.WriteDebugInfo(@"");

if((resultBA80C2CD020D491F8055BD63F5ADE3A5!=null) && (resultBA80C2CD020D491F8055BD63F5ADE3A5.Count!=0))
{
if(resultBA80C2CD020D491F8055BD63F5ADE3A5.ContainsKey(7))
ISpace["MF_SanctionedAmount"].Value = resultBA80C2CD020D491F8055BD63F5ADE3A5[7];
if(resultBA80C2CD020D491F8055BD63F5ADE3A5.ContainsKey(6))
ISpace["MF_SanctionedCount"].Value = resultBA80C2CD020D491F8055BD63F5ADE3A5[6];
if(resultBA80C2CD020D491F8055BD63F5ADE3A5.ContainsKey(5))
ISpace["MF_ProposalAmount"].Value = resultBA80C2CD020D491F8055BD63F5ADE3A5[5];
if(resultBA80C2CD020D491F8055BD63F5ADE3A5.ContainsKey(0))
ISpace["MF_ActiveCount"].Value = resultBA80C2CD020D491F8055BD63F5ADE3A5[0];
if(resultBA80C2CD020D491F8055BD63F5ADE3A5.ContainsKey(3))
ISpace["MF_CompletedAmount"].Value = resultBA80C2CD020D491F8055BD63F5ADE3A5[3];
if(resultBA80C2CD020D491F8055BD63F5ADE3A5.ContainsKey(1))
ISpace["MF_ActiveAmount"].Value = resultBA80C2CD020D491F8055BD63F5ADE3A5[1];
if(resultBA80C2CD020D491F8055BD63F5ADE3A5.ContainsKey(2))
ISpace["MF_CompletedCount"].Value = resultBA80C2CD020D491F8055BD63F5ADE3A5[2];
if(resultBA80C2CD020D491F8055BD63F5ADE3A5.ContainsKey(4))
ISpace["MF_ProposalCount"].Value = resultBA80C2CD020D491F8055BD63F5ADE3A5[4];
}
else{
ISpace["MF_SanctionedAmount"].Value = null;ISpace["MF_SanctionedCount"].Value = null;ISpace["MF_ProposalAmount"].Value = null;ISpace["MF_ActiveCount"].Value = null;ISpace["MF_CompletedAmount"].Value = null;ISpace["MF_ActiveAmount"].Value = null;ISpace["MF_CompletedCount"].Value = null;ISpace["MF_ProposalCount"].Value = null;
}
}
}
catch(Exception ex)
{
base.WriteErrorInfo(@"Exception:",ex);
}
}
private void SubscribeElementEvents_mf_projectlistactiveprojectspr_gridrows (ref Dictionary<string,ServiceElementData> ISpace)
{
IISpace iSpace = new ISpace();
try
{
base.WriteDebugInfo(@"MF_ProjectListActiveProjectspr_GridRows-OnChange");
if(1==1)
{
base.WriteDebugInfo(@"if(1==1)");

var querySourceDE7234F0989946C58758427FAF6AEB5A =GetQueryExpressionDataSource("DE7234F0-9899-46C5-8758-427FAF6AEB5A");

DataTable resultDE7234F0989946C58758427FAF6AEB5A=iSpace.SetGridDataSource(querySourceDE7234F0989946C58758427FAF6AEB5A,@"EXEC dbo.[GetProjectExplorerDetails] 1,'" + ISpace["F_ProjectDetailsId"].Value + @"','','','" + ISpace["MF_ProjectListActiveProjectspr_GridRows"].Value + @"',''");


base.WriteDebugInfo(@"var querySourceDE7234F0989946C58758427FAF6AEB5A =GetQueryExpressionDataSource(""DE7234F0-9899-46C5-8758-427FAF6AEB5A"");DataTable resultDE7234F0989946C58758427FAF6AEB5A=iSpace.SetGridDataSource(querySourceDE7234F0989946C58758427FAF6AEB5A,@""EXEC dbo.[GetProjectExplorerDetails] 1,'"" + ISpace[""F_ProjectDetailsId""].Value + @""','','','"" + ISpace[""MF_ProjectListActiveProjectspr_GridRows""].Value + @""',''"");");
base.WriteDebugInfo(@"EXEC dbo.[Get_bccb6bb6-39f0-4863-8a17-2efdffe2a45b_FilterData] 1,'#F_ProjectDetailsId','',#MF_ProjectListActiveProjectspr_PageIndex,'#MF_ProjectListActiveProjectspr_GridRows'");


List<Triplet<string, short, short?>> result28E58773E71A46B18591A5E00111B3CB=acdataIspace34CF8FAF7574478F9C4AA65F98E4D18A.GetQueryExpressionBindings("28E58773-E71A-46B1-8591-A5E00111B3CB");
iSpace.SetGridData(resultDE7234F0989946C58758427FAF6AEB5A,result28E58773E71A46B18591A5E00111B3CB,"ProjectListActiveProjectspr",ref ISpace);
}
}
catch(Exception ex)
{
base.WriteErrorInfo(@"Exception:",ex);
}
}
private void SubscribeElementEvents_cancel (ref Dictionary<string,ServiceElementData> ISpace)
{
IISpace iSpace = new ISpace();
try
{
base.WriteDebugInfo(@"Cancel-OnClick");
if(1==1)
{
base.WriteDebugInfo(@"if(1==1)");
if(ISpace["FormVersionId"].Value=="02A1FE25-AE94-461D-9B6F-A7AC8493B6F6")
{
ISpace["UI_GridPanel"].Visible="true";ISpace["UI_GridPanel"].Enbl="true";
}
base.WriteDebugInfo(@"if(ISpace[""FormVersionId""].Value==""02A1FE25-AE94-461D-9B6F-A7AC8493B6F6"")
{
ISpace[""UI_GridPanel""].Visible=""true"";ISpace[""UI_GridPanel""].Enbl=""true"";
}");
}
}
catch(Exception ex)
{
base.WriteErrorInfo(@"Exception:",ex);
}
}
public override void ExecuteMethod
	(string methodName, string elementName,
		ref Dictionary<string, ServiceElementData> dfsParam)
{
	if(methodName.ToLower().Equals("formonload"))
{
			SubscribeFormEvents_root(ref dfsParam);
}
	if(methodName.ToLower().Equals("onchange"))
{
		if(elementName.ToLower().Equals("mf_projectlistactiveprojectspr_pageindex"))
    {
    			SubscribeElementEvents_mf_projectlistactiveprojectspr_pageindex(ref dfsParam);
    }
		if(elementName.ToLower().Equals("mf_projectlistactiveprojectspr_gridrows"))
    {
    			SubscribeElementEvents_mf_projectlistactiveprojectspr_gridrows(ref dfsParam);
    }
}
	if(methodName.ToLower().Equals("onclick"))
{
		if(elementName.ToLower().Equals("add"))
{
			SubscribeElementEvents_add(ref dfsParam);
}
		if(elementName.ToLower().Equals("mgg_v1_projecttitle"))
{
			SubscribeElementEvents_mgg_v1_projecttitle(ref dfsParam);
}
		if(elementName.ToLower().Equals("save"))
{
			SubscribeElementEvents_save(ref dfsParam);
}
		if(elementName.ToLower().Equals("edit"))
{
			SubscribeElementEvents_edit(ref dfsParam);
}
		if(elementName.ToLower().Equals("apply"))
{
			SubscribeElementEvents_apply(ref dfsParam);
}
		if(elementName.ToLower().Equals("cancel"))
{
			SubscribeElementEvents_cancel(ref dfsParam);
}
}
}
}
}

  







namespace CPS.Proof.DFSExtension
{
	
using System;
    using System.Collections.Generic;    
    using System.Text;	
    using System.Linq;	
	using SRA.Proof.Infrastructure;
	using SRA.Proof.Helpers;


	public partial class AcDataISpace34CF8FAF7574478F9C4AA65F98E4D18A  : ExtensionActivityBase
	{		
		
		
		private Dictionary<string,  List<Triplet<string, short, short?>>> queryExpressionBindings = 
											new Dictionary<string,  List<Triplet<string, short, short?>>>
		{
							{"28E58773-E71A-46B1-8591-A5E00111B3CB", 
				
				new List<Triplet<string, short, short?>> {

								   new Triplet<string, short, short?>("MGG_V1_Agency",7,
														   -1),
								   new Triplet<string, short, short?>("MGG_V1_CoPI",5,
														   -1),
								   new Triplet<string, short, short?>("MGG_V1_Department",3,
														   -1),
								   new Triplet<string, short, short?>("MGG_V1_Duration",10,
														   -1),
								   new Triplet<string, short, short?>("MGG_V1_EndDate",9,
														   -1),
								   new Triplet<string, short, short?>("MGG_V1_FinancialYear",11,
														   -1),
								   new Triplet<string, short, short?>("MGG_V1_PI",4,
														   -1),
								   new Triplet<string, short, short?>("MGG_V1_ProjectDetailsID",0,
														   -1),
								   new Triplet<string, short, short?>("MGG_V1_ProjectNo",2,
														   -1),
								   new Triplet<string, short, short?>("MGG_V1_ProjectTitle",1,
														   -1),
								   new Triplet<string, short, short?>("MGG_V1_ProjectType",6,
														   -1),
								   new Triplet<string, short, short?>("MGG_V1_SanctionedValue",12,
														   -1),
								   new Triplet<string, short, short?>("MGG_V1_StartDate",8,
														   -1),
								   new Triplet<string, short, short?>("MGG_V1_Type",14,
														   -1),
								}
			},
					};	

	

		private Dictionary<string,string> formVersionList =new Dictionary<string,string> 
		{
							{"02A1FE25-AE94-461D-9B6F-A7AC8493B6F6", "Medium"},
					};	

		
		private Dictionary<string,string> formVersionLayoutList =new Dictionary<string,string> 
		{
							{"4BEC0590-061E-4553-B58B-2D7108C79085", ""},
							{"B7189E15-4C06-428C-859F-AA2E9297C20F", ""},
							{"02A1FE25-AE94-461D-9B6F-A7AC8493B6F6", ""},
							{"8671B05B-41A1-496B-8BAB-015A75C43A23", ""},
							{"B25A31B0-E585-4BB5-9555-126CB0502E1F", ""},
							{"A2108E21-BD18-4432-AE25-F2F1E294AC4E", ""},
							{"ECB2F7D4-C658-45DF-9C5E-A71D786AF743", ""},
							{"28380647-2267-49C9-95D9-AE4F9DA5CFAD", ""},
					};	

		

	
	
		
		
		public override  List<Triplet<string, short, short?>> GetQueryExpressionBindings(string expressionId)
		{
			if(string.IsNullOrWhiteSpace(expressionId))
            {
			  return null;				
            }


			if(queryExpressionBindings == null || queryExpressionBindings.Count <= 0)
				return null;

			return queryExpressionBindings[expressionId];
		}

		public override string GetValidFormVersionId(string formVersionId,ViewportTypes viewPort)
		{	
					
			 if(formVersionList == null || formVersionList.Count <= 0)
				return formVersionId;

			if (formVersionList.Count == 1)
                return formVersionList.FirstOrDefault().Key;  

			

			if ( !string.IsNullOrEmpty(formVersionId) &&  formVersionList.ContainsKey(formVersionId))
            {
                if (formVersionList.Any(x => x.Key == formVersionId && x.Value == viewPort.ToString()))
                    return formVersionId;
            }

			var fallback= new List<string>{"Mobile","Tab","Medium","Large"};

           switch (viewPort)
            {
                case ViewportTypes.Tab:
                    fallback = new List<string> { "Tab", "Mobile", "Medium", "Large" };
                    break;

                case ViewportTypes.Medium:
                    fallback = new List<string> { "Medium", "Tab", "Large", "Mobile" };
                    break;

                case ViewportTypes.Large:
                    fallback = new List<string> { "Large", "Medium", "Tab", "Mobile" };
                    break;
            }

           for (int i = 0; i < fallback.Count; i++)
           {
               if (formVersionList.Any(x => x.Value == fallback[i]))
                   return formVersionList.Where(x => x.Value == fallback[i]).Select(y => y.Key).FirstOrDefault();
           }

		   return formVersionId;
		}

	

		
		
		
		
		


	}

}
		


