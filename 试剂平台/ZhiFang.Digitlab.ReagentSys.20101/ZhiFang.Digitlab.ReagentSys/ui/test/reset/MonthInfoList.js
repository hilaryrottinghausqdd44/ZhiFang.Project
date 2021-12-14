Ext.ns('Ext.iqc');
Ext.Loader.setConfig({enabled: true});//允许动态加载
Ext.Loader.setPath('Ext.zhifangux',getRootPath()+'/ui/zhifangux');
Ext.define('Ext.iqc.month.InfoList',{
	extend:'Ext.zhifangux.BasicGridPanel',
	alias:'widget.iqcmonthinfolist',
	requires: ['Ext.iqc.month.YearMonthComponent'],
	
	title:'月质控列表',
	width:800,
	hieght:600,
	
	/**基础标题*/
    defaultTitle:'',
    /**是否循环定位光标*/
	isCycle:true,
	/**默认加载数据*/
	defaultLoad:false,
    /**服务地址*/
	serverUrl:{
		/**获取预备数据*/
		searchPreliminaryData:getRootPath()+'/QCService.svc/QC_RJ_GetJudgeLoseControlBackupValue',
		/**质控失控判断*/
		judgeLoseControlValue:getRootPath()+'/QCService.svc/QC_RJ_JudgeLoseControlValue',
		/**获取列表*/
		search:[
			getRootPath()+'/QCService.svc/QC_BA_GetQCDValueByQCMatAndDate',//仪器-质控物
			getRootPath()+'/QCService.svc/QC_BA_GetQCDValueByItemAndDate'//仪器-质控项目
		],
		/**新增质控数据*/
		add:getRootPath()+'/QCService.svc/QC_BA_ADDQCDataCustom',
		/**修改质控数据*/
		update:getRootPath()+'/QCService.svc/QC_BA_UpdateQCDataCustom',
		/**根据ID删除质控数据*/
		del:getRootPath()+'/QCService.svc/QC_UDTO_DelQCDValue',
		/**逻辑删除*/
		updateIsUse:getRootPath()+'/QCService.svc/QC_UDTO_UpdateQCDValueByField'
	},
	/**条件的ID属性*/
	tid:'',
	/**仪器ID*/
	pid:'',
	/**服务类型,1:质控物;2:质控项目*/
	urlType:1,
	/**旧的条件*/
	oldParams:{},
	
	
	
	
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		me.initListeners();//初始化监听
	},
	/**
	 * 初始化面板属性
	 * @private
	 */
	initComponent:function(){
		var me = this;
		me.columns = me.createColumns();//创建数据列内容
		me.dockedItems = me.createDockedItems();//创建挂靠功能
		me.callParent(arguments);
	},
	/**
	 * 创建数据列内容
	 * @private
	 * @return {}
	 */
	createColumns:function(){
		var me = this;
		var columns = [];
		return columns;
	},
	/**
	 * 创建挂靠功能
	 * @private
	 * @return {}
	 */
	createDockedItems:function(){
		var me = this;
		var dockedItems = [{
			xtype:'toolbar',dock:'top',itemId:'toptoolbar',
			items:[{xtype:'iqcyearmonthcomponent',itemId:'yearmonth',value:new Date()}]
		},{
			xtype:'toolbar',dock:'bottom',itemId:'bottomtoolbar',
			items:[{
				itemId:'add',text:'新增',iconCls:'build-button-add'
			},{
				itemId:'edit',text:'修改',iconCls:'build-button-edit'
			},{
				itemId:'delete',text:'删除',iconCls:'build-button-delete'
			},{
				itemId:'save',text:'保存',iconCls:'build-button-save',handler:function(){me.save();}
			},'-',{
				xtype:'checkbox',
				itemId:'mark',
				boxLabel:'标记质控数据'
			},{
				xtype:'checkbox',
				itemId:'notused',
				boxLabel:'显示不使用质控数据'
			}]
		}];
		return dockedItems;
	},
	/**
	 * 初始化监听
	 * @private
	 */
	initListeners:function(){
		var me = this;
		
	},
	/**
	 * 保存
	 * @private
	 */
	save:function(){
		var me = this;
		var toptoolbar = me.getComponent('toptoolbar');
		var date = toptoolbar.getComponent('yearmonth').getValue();
		
		var start = date.start;
		var end = date.end;
		alert(start.toString() + ";" + end.toString());
	}
});