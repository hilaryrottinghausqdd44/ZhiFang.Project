/**
 * 人员选择列表
 * @author Jcall
 * @version 2019-12-19
 */
Ext.define('Shell.class.basic.sampleType.SampleType',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'样本类型选择列表',
	
	//获取数据服务路径
	selectUrl:'/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSampleTypeByHQL?isPlanish=true',
	//系统编码
	SystemCode:'ZF_LAB_START',
	
	//序号列宽度
	rowNumbererWidth:35,
    //是否单选
	checkOne:true,
	
	//类型编码
	TSysCode:null,
	
	//排序
	defaultOrderBy: [{ property:'LBSampleType_DispOrder',direction:'ASC'}],
    
	initComponent:function(){
		var me = this;
		
		me.defaultWhere = me.defaultWhere || '';
		
		if(me.defaultWhere){
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += "lbsampletype.IsUse=1 ";
		
		//查询框信息
		me.searchInfo = { width: 145, emptyText: '名称', isLike: true, fields: ['lbsampletype.CName']};
		
		//数据列
		me.columns = [{
			text: '名称', dataIndex:'LBSampleType_CName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
				text: '编码', dataIndex:'LBSampleType_StandCode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
				text: '主键ID', dataIndex:'LBSampleType_Id',isKey:true,hidden:true,hideable:false
		}];
		
		me.callParent(arguments);
	},
	//获取带查询参数的URL
	getLoadUrl: function(){
		var me = this,
			url = me.callParent(arguments),
			exp = new RegExp(JShell.System.Path.ROOT,"g");
		
		//url = url.replace(exp,JShell.System.Path.LIIP_ROOT);
		
		return url;
	}
});