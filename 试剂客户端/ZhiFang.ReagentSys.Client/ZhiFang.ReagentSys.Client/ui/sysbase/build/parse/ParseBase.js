/**
 * 解析器基类
 * @author Jcall
 * @version 2014-08-19
 */
Ext.define('Shell.sysbase.build.ParserBase',{
	mixins:['Shell.ux.server.Ajax','Shell.ux.PanelController'],
	
	/**新增应用信息的服务地址*/
	addUrl:'/ConstructionService.svc/CS_UDTO_AddBTDAppComponents',
	/**修改应用信息的服务地址*/
	editUrl:'/ConstructionService.svc/CS_UDTO_UpdateBTDAppComponents',
	/**获取应用信息的服务地址*/
	getUrl:'/ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsById',
	
	/**根据ID获取应用信息*/
	getInfoById:function(id){
		var me = this,
			url = Shell.util.Path.rootPath + me.getUrl + '?id=' + id;
			
		if(id == null){
			me.showError('Shell.sysbase.build.ParserBase的getInfoById方法id参数不存在！');
			return;
		}
		
		me.getToServer(url,function(text){
			
		});
	},
	
	
	
	/**需要提交的数据对象*/
	BTDAppComponents:{
		Id:'',//主键ID
		CName:'',//中文名称,[唯一]
		EName:'',//英文名称,[唯一]
		BuildType:'',//构建类型,[构建/定制]
		AppType:'',//应用类型,[列表/表单/应用/...]
		ModuleOperCode:'',//功能编码,[唯一]
		ModuleOperInfo:'',//功能简介
		
		
		DesignCode:'',//设计代码,存放需要生成的还原文件内容,[固定目录]==不存数据
		ClassCode:'',//类代码,存放需要生成的类文件内容,[固定目录]==不存数据
		
		Creator:'',//创建者
		Modifier:'',//修改者
		PinYinZiTou:'',//拼音字头
		DataUpdateTime:'',//更新时间
		LabID:'',//LabID
		DataAddTime:'',//创建时间
		DataTimeStamp:'',//时间戳
		
		BTDAppComponentsOperateList:[{
			Id:'',//应用操作ID
			AppComOperateKeyWord:'',//应用操作编号
			AppComOperateName:'',//应用操作名称
			DataTimeStamp:'',//应用操作时间戳
			LabID:'',//关系表实验室ID
			Creatoro:'',
			Modifier:'',
			DataAddTime:'',
			DataUpdateTime:'',
			RowFilterBase:''
		}],
		BTDAppComponentsRefList:[{
			RefAppComID:'',//被引用应用GUID
			RefAppComIncID:'',//被引用应用内部ID
			LabID:'',//关系表实验室ID
			Id:'',//关系表GUID
			DataTimeStamp:'',//关系表时间戳
			Creatoro:'',
			Modifier:'',
			DataAddTime:'',
			DataUpdateTime:''
		}]
	},
	/**完整的应用对象*/
	BTDAppComponents:{
		Id:'',//主键ID
		CName:'',//中文名称
		DesignCode:'',//设计代码==================
		ClassCode:'',//类代码=====================
		ModuleOperCode:'',//功能编码
		ModuleOperInfo:'',//功能简介
		InitParameter:'',//初始化参数**************
		AppType:'',//应用类型
		
		EName:'',//英文名称
		BuildType:'',//构建类型
		Creator:'',//创建者
		Modifier:'',//修改者
		PinYinZiTou:'',//拼音字头
		DataUpdateTime:'',//更新时间
		LabID:'',//LabID
		DataAddTime:'',//创建时间
		DataTimeStamp:'',//时间戳
		
		BTDAppComponentsOperateList:[{
			Id:'',//应用操作ID
			AppComOperateKeyWord:'',//应用操作编号
			AppComOperateName:'',//应用操作名称
			DataTimeStamp:'',//应用操作时间戳
			LabID:'',//关系表实验室ID
			Creatoro:'',
			Modifier:'',
			DataAddTime:'',
			DataUpdateTime:'',
			RowFilterBase:''
		}],
		BTDAppComponentsRefList:[{
			RefAppComID:'',//被引用应用GUID
			RefAppComIncID:'',//被引用应用内部ID
			LabID:'',//关系表实验室ID
			Id:'',//关系表GUID
			DataTimeStamp:'',//关系表时间戳
			Creatoro:'',
			Modifier:'',
			DataAddTime:'',
			DataUpdateTime:''
		}]
	}
});