/**
 * 请假类型选择
 * @author liangyl	
 * @version 2017-01-24
 */
Ext.define('Shell.class.oa.at.attendance.statistical.empdetail.basic.LeaveTypeCheckGrid', {
    extend:'Shell.ux.grid.CheckPanel',
    title:'请假类型选择列表',
    width:280,
    height:310,
    /**是否带清除按钮*/
	hasClearButton:true,
    /**获取数据服务路径*/
	selectUrl:'/WeiXinAppService.svc/ST_UDTO_SearchATApproveStatusByHQL?isPlanish=true',
    /**默认排序字段*/
	defaultOrderBy: [{
		property: 'ATApproveStatus_Name',
		direction: 'ASC'
	}],
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
        this.enableControl();
	},
	 /**请假类型*/
	ATEmpApplyAllLogList:[
	    {Id: '10302',Name:'病假'},
		{Id: '10303',Name:'补签卡'},
		{Id: '10304',Name:'产假'},
		{Id: '10305',Name:'调休'},
		{Id: '10307',Name:'婚假'},
		{Id: '10308',Name:'年休假'},
		{Id: '10309',Name:'丧假'},
		{Id: '10310',Name:'事假'}
	],
    /**是否单选*/
	checkOne:false,
    /**默认加载数据*/
	defaultLoad: false,
	/**创建数据集*/
	createStore: function() {
		var me = this;
		return Ext.create('Ext.data.Store', {
			fields: ['Id','Name'],
		    data : me.ATEmpApplyAllLogList
		});
	},
	initComponent:function(){
		var me = this;
		me.defaultWhere = me.defaultWhere || '';
		if(me.defaultWhere){
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}

		//查询框信息
		me.searchInfo = {width:145,emptyText:'名称',isLike:true,
			fields:['atapprovestatus.Name']};
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	initButtonToolbarItems:function(){
		var me = this;
		me.buttonToolbarItems = me.buttonToolbarItems || [];
		if(me.hasClearButton){
			me.buttonToolbarItems.unshift({
				text:'清除',iconCls:'button-cancel',tooltip:'<b>清除原先的选择</b>',
				handler:function(){me.fireEvent('accept',me,null);}
			},'->');
		}
		if(me.hasAcceptButton){
			me.buttonToolbarItems.push('->','accept');
		}
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		var columns = [{
			text:'名称',dataIndex:'Name',width:150,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'主键ID',dataIndex:'Id',isKey:true,hidden:true,hideable:false
		}]
		return columns;
	}
});