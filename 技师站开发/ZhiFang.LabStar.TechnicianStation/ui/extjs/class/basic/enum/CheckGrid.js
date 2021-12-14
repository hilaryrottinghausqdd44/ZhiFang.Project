/**
 * 枚举选择列表
 * @author Jcall
 * @version 2019-12-19
 */
Ext.define('Shell.class.basic.enum.CheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'枚举选择列表',
    width:260,
    height:600,
	
	//获取数据服务路径
	selectUrl:'/ServerWCF/CommonService.svc/GetClassDic',
	//默认域名
	ClassNameSpace:'ZhiFang.Entity.LabStar',
	//枚举名
	EnumName:'',
	
	//序号列宽度
	rowNumbererWidth:35,
    //是否单选
	checkOne:true,
	//带分页栏
	hasPagingtoolbar:false,
	//查询框信息
	searchInfo:{emptyText:'',isLike:true,fields:[],width:135},
    
	initComponent:function(){
		var me = this;
		
		me.selectUrl += '?classnamespace=' + me.ClassNameSpace + '&classname=' + me.EnumName;
		
		//数据列
		me.columns = [{
			text:'名称',dataIndex:'Name',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'编码',dataIndex:'Code',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'主键ID',dataIndex:'Id',isKey:true,hidden:true,hideable:false
		}];
		
		me.callParent(arguments);
	}
});