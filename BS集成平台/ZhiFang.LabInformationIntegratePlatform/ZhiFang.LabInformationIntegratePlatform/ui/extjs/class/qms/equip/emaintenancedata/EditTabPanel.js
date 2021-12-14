/**
 * 质量记录登记录入
 * @author liangyl
 * @version 2016-08-25
 */
Ext.define('Shell.class.qms.equip.emaintenancedata.EditTabPanel', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '质量记录登记录入',
	width: 500,
	height: 400,
	TempletID: null,
	/**月保养*/
	TempletType: '',
	/**月保养编码*/
	TempletTypeCode: '',
	//从外边传参时间控件是否只读,默认是true，不可改, false（可改） 
    ISEDITDATE:true,
    TEMPTLETTYPE:'',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
//		me.Form.showMask();//显示遮罩层
		me.OperatePanel.on({
			onAddClick:function(grid){
				me.fireEvent('onAddClick', grid);
			},
			selectclick:function(record,grid,fields){
				me.fireEvent('selectclick',record, grid,fields);
			},
			itemclick:function(record,grid,fields){
				me.fireEvent('itemclick',record, grid,fields);
			},
			loadsearch:function(list){
				me.fireEvent('loadsearch',list);
				//me.Form.showMask();//显示遮罩层
			},
			nodata:function(){
				me.fireEvent('nodata',me.Form);
			},
			onDelClick:function(){
				me.fireEvent('onDelClick');
			}
		});
		me.Form.on({
			blur:function(com){
			    me.fireEvent('blur',com);
			},
			onDailyClick:function(com){
				me.fireEvent('onDailyClick',com);
			}
		});
		
	},
	initComponent: function() {
		var me = this;
		me.addEvents('loadsearch');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		var items = [];
		me.Form = Ext.create('Shell.class.qms.equip.register.Form', {
			border: true,
			title: '表单',
			region: 'center',
		    //从外边传参时间控件是否只读,默认是true，不可改, false（可改） 
            ISEDITDATE:me.ISEDITDATE,
            TEMPTLETTYPE:me.TEMPTLETTYPE,
			header: false,
			itemId: 'Form'
		});
		me.OperatePanel= Ext.create('Shell.class.qms.equip.register.TBPanel', {
			title: '列表',
		    region: 'east',
			width:250,
			split: true,
			collapsible: true,
			collapseMode:'mini',
			header: false,
			hidden:true,
			itemId: 'OperatePanel'
		});
		
		return [me.Form,me.OperatePanel];
	},
	//获取选中行
	getGridSelect:function(){
		var me = this;
		return me.OperatePanel.getGridSelect();
	},
	onSearch:function(TempletID,Operatedate,NowTabType,IsTbType){
		var me = this;
		me.IsTbType = IsTbType;
		me.OperatePanel.onSearch(TempletID,Operatedate,NowTabType);
	}
});