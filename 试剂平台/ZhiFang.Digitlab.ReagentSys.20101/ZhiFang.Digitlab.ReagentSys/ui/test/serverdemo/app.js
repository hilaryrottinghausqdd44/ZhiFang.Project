Ext.onReady(function(){
	Ext.QuickTips.init();//初始化后就会激活提示功能
	Ext.Loader.setConfig({enabled: true});//允许动态加载
	
	var panel = Ext.create('Ext.demo.ModelDemo',{
		//功能名称
		title:'国家服务测试',
		//获取数据列表的服务地址
		getListUrl:getRootPath()+'/SingleTableService.svc/ST_UDTO_SearchBCountryByHQL',
		//根据ID获取一条数据信息的服务地址
		getInfoByIdUrl:getRootPath()+'/SingleTableService.svc/ST_UDTO_SearchBCountryById',
		//根据ID删除一条信息的服务地址
		deleteByIdUrl:getRootPath()+'/SingleTableService.svc/ST_UDTO_DelBCountry',
		//新增一条数据的服务地址
		addUrl:getRootPath()+'/SingleTableService.svc/ST_UDTO_AddBCountry',
		//更新一条数据的服务地址[按字段保存]
		updateByFieldsUrl:getRootPath()+'/SingleTableService.svc/ST_UDTO_UpdateBCountryByField',
		//更新一条数据的服务地址[实体保存]
		updateUrl:getRootPath()+'/SingleTableService.svc/ST_UDTO_UpdateBCountry',
		
		//国家对象所有的字段
		fields:[
			{key:'BCountry_Name',value:'名称'},
			{key:'BCountry_SName',value:'简称'},
			{key:'BCountry_Shortcode',value:'快捷码'},
			{key:'BCountry_PinYinZiTou',value:'汉子拼音字头'},
			{key:'BCountry_Comment',value:'备注'},
			{key:'BCountry_IsUse',value:'是否使用'},
			{key:'BCountry_Id',value:'主键ID'},
			{key:'BCountry_LabID',value:'实验室ID'},
			{key:'BCountry_DataAddTime',value:'数据加入时间'},
			{key:'BCountry_DataTimeStamp',value:'时间戳'}
		],
		//对象名称
		objectName:'国家',
		//主键字段
		key:'BCountry_Id'
	});
	//总体布局
	Ext.create('Ext.container.Viewport',{
		padding:2,
		layout:'fit',
		items:[panel]
	});
});