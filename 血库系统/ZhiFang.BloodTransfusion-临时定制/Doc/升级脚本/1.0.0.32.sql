
IF COL_LENGTH('NPUser', 'LabID') IS NULL ALTER TABLE NPUser ADD LabID bigint;

IF COL_LENGTH('NPUser', 'DataAddTime') IS NULL ALTER TABLE NPUser ADD DataAddTime datetime; 

IF COL_LENGTH('NPUser', 'DataTimeStamp') IS NULL ALTER TABLE NPUser ADD DataTimeStamp timestamp;

 Update NPUser set LabID=0 where LabID is null; 

IF COL_LENGTH('B_Parameter', 'ItemEditInfo') IS NULL ALTER TABLE B_Parameter ADD ItemEditInfo ntext; 


--输血申请单当前流水号
update B_Parameter set SName='医生站',ItemEditInfo=N'{"ItemXType":"textfield","ItemDefaultValue":"","ItemUnit":"","ItemDataSet":""}' where ParameterID=5691564273202278074 and ItemEditInfo is null;

--启用用户UI配置
update B_Parameter set ItemEditInfo=N'{"ItemXType":"radiogroup","ItemDefaultValue":"0","ItemUnit":"","ItemDataSet":"[{''1'':''是''},{''0'':''否''}]"}' where ParameterID=5524339378542032882 and ItemEditInfo is null;

--更新输血过程登记时是否添加操作记录
update B_Parameter set ItemEditInfo=N'{"ItemXType":"radiogroup","ItemDefaultValue":"0","ItemUnit":"","ItemDataSet":"[{''1'':''是''},{''0'':''否''}]"}' where ParameterID=5214493501844260901 and ItemEditInfo is null;

--是否输血过程记录登记后才能血袋回收登记
update B_Parameter set ItemEditInfo=N'{"ItemXType":"radiogroup","ItemDefaultValue":"1","ItemUnit":"","ItemDataSet":"[{''1'':''是''},{''0'':''否''}]"}' where ParameterID=5214493501844260900 and ItemEditInfo is null;

--CS服务访问URL
update B_Parameter set ItemEditInfo=N'{"ItemXType":"textfield","ItemDefaultValue":"","ItemUnit":"","ItemDataSet":""}' where ParameterID=5214493501844260811 and ItemEditInfo is null;

--集成平台服务访问URL
update B_Parameter set ItemEditInfo=N'{"ItemXType":"textfield","ItemDefaultValue":"","ItemUnit":"","ItemDataSet":""}' where ParameterID=5214493501844260801 and ItemEditInfo is null;

--血袋接收是否需要护工完成取血确认
update B_Parameter set ItemEditInfo=N'{"ItemXType":"radiogroup","ItemDefaultValue":"0","ItemUnit":"","ItemDataSet":"[{''1'':''是''},{''0'':''否''}]"}' where ParameterID=5101233254823494116 and ItemEditInfo is null;

--列表默认分页记录数
update B_Parameter set ItemEditInfo=N'{"ItemXType":"numberfield","ItemDefaultValue":"10","ItemUnit":"","ItemDataSet":""}' where ParameterID=5085688375696300791 and ItemEditInfo is null;

--登录后升级数据库
update B_Parameter set ItemEditInfo=N'{"ItemXType":"radiogroup","ItemDefaultValue":"1","ItemUnit":"","ItemDataSet":"[{''1'':''是''},{''0'':''否''}]"}' where ParameterID=5081884485807967905 and ItemEditInfo is null;

--实验室数据升级版本
update B_Parameter set ItemEditInfo=N'{"ItemXType":"textfield","ItemDefaultValue":"","ItemUnit":"","ItemDataSet":""}' where ParameterID=5000278512090896900 and ItemEditInfo is null;


