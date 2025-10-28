#!/bin/bash
sudo systemctl stop TestApplication
echo "Service stopped"
cp /home/cpsadmin/Presentations/iProofService/Castle.Core.dll /home/cpsadmin/Presentations/DynamicDFSAPI/testGrid/SupportDLL
cp /home/cpsadmin/Presentations/iProofService/Castle.Windsor.dll /home/cpsadmin/Presentations/DynamicDFSAPI/testGrid/SupportDLL
cp /home/cpsadmin/Presentations/iProofService/CommonInterface.dll /home/cpsadmin/Presentations/DynamicDFSAPI/testGrid/SupportDLL
cp /home/cpsadmin/Presentations/iProofService/ConfigSectionHandlers.dll /home/cpsadmin/Presentations/DynamicDFSAPI/testGrid/SupportDLL
cp /home/cpsadmin/Presentations/iProofService/Cryptography.dll /home/cpsadmin/Presentations/DynamicDFSAPI/testGrid/SupportDLL
cp /home/cpsadmin/Presentations/iProofService/log4net.dll /home/cpsadmin/Presentations/DynamicDFSAPI/testGrid/SupportDLL
cp /home/cpsadmin/Presentations/iProofService/Microsoft.Practices.EnterpriseLibrary.Common.dll /home/cpsadmin/Presentations/DynamicDFSAPI/testGrid/SupportDLL
cp /home/cpsadmin/Presentations/iProofService/Microsoft.Practices.EnterpriseLibrary.Data.dll /home/cpsadmin/Presentations/DynamicDFSAPI/testGrid/SupportDLL
cp /home/cpsadmin/Presentations/iProofService/Newtonsoft.Json.dll /home/cpsadmin/Presentations/DynamicDFSAPI/testGrid/SupportDLL
cp /home/cpsadmin/Presentations/iProofService/ObjectFactory.dll /home/cpsadmin/Presentations/DynamicDFSAPI/testGrid/SupportDLL
cp /home/cpsadmin/Presentations/iProofService/ProofExceptions.dll /home/cpsadmin/Presentations/DynamicDFSAPI/testGrid/SupportDLL
cp /home/cpsadmin/Presentations/iProofService/SpanJson.dll /home/cpsadmin/Presentations/DynamicDFSAPI/testGrid/SupportDLL
cp /home/cpsadmin/Presentations/iProofService/Types.dll /home/cpsadmin/Presentations/DynamicDFSAPI/testGrid/SupportDLL


dotnet build /home/cpsadmin/Presentations/DynamicDFSAPI/testGrid/iProofProcessService/iProofProcessService.csproj
sudo systemctl restart TestApplication
