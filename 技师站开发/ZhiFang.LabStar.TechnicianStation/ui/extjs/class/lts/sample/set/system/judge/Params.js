/**
 * 系统判定设置-页签
 * @author Jcall
 * @version 2020-06-30
 * @desc liangyl 2021-03-24修改 createItems时添加几个属性
 */
Ext.define('Shell.class.lts.sample.set.system.judge.Params',{
    extend:'Ext.form.Panel',
    title:'系统判定设置-页签',
    bodyPadding:10,
    
	//获取常规检验参数分类列表
	getListUrl:'/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_QuerySystemDefaultPara?isPlanish=false',
	 //获取采样出厂设置
    getFactoryUrl:'/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_QueryFactorySettingPara?isPlanish=false',

	//保存参数
	editUrl:'',
	//获取的字段
	ParaFilds:[
		'ParaNo','CName','TypeCode','ParaType','ParaDesc','ParaEditInfo','SystemCode',
		'ShortCode','BVisible','BVisible','IsUse','ParaValue','Id','DispOrder'
	],
	//类型编码
	TypeCode:'',
	
    //默认排序字段
	defaultOrderBy:[{property:"BPara_DispOrder",direction:"ASC"}],
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		me.loadEnumData();
	},
	initComponent:function(){
		var me = this;
		me.callParent(arguments);
	},
	createItems: function(list) {
		var me = this,
			len = list.length,
			items = [];
		
		for(var i=0;i<len;i++){
			items.push({
				boxLabel:list[i].CName,
				name:list[i].ParaNo,
				xtype:'checkbox',
				checked:list[i].ParaValue=="1" ? true : false,
				ParaID:list[i].Id,
				ParaEditInfo:list[i].ParaEditInfo,
				ParaType:list[i].ParaType,
				ShortCode:list[i].ShortCode,
				SystemCode:list[i].SystemCode,
				TypeCode:list[i].TypeCode
			});
		}
		
		me.add(items);
	},
	loadEnumData:function(){
		var me = this,
			url = JShell.System.Path.ROOT + me.getListUrl;
			
		url += '&paraTypeCode=' + me.TypeCode + '&fields=BPara_' + me.ParaFilds.join(',BPara_');
		url+='&sort='+JShell.JSON.encode(me.defaultOrderBy);
		
		JShell.Server.get(url,function(data){
			if(data.success){
				me.createItems((data.value || {}).list || []);
			}else{
				me.update('<div style="padding:50px 10px;text-align:center;">' + data.msg + '</div>');
			}
		});
	},
	//采用默认设置
	loadFactoryData:function(){
		var me = this,
			url = JShell.System.Path.ROOT + me.getFactoryUrl;
			
		url += '&paraTypeCode=' + me.TypeCode + '&fields=BPara_' + me.ParaFilds.join(',BPara_');
		url+='&sort='+JShell.JSON.encode(me.defaultOrderBy);
		
		JShell.Server.get(url,function(data){
			if(data.success){
				var list = (data.value || {}).list || [];
				var obj = {};
				for(var i=0;i<list.length;i++){
					obj[list[i].ParaNo] =  list[i].ParaValue;					
				}
               me.getForm().setValues(obj);
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	}
});