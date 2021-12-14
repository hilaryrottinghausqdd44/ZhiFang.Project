/**
 * 选择需要打印的检验单
 * @author liangyl
 * @version 2019-11-20
 */
Ext.define('Shell.class.lts.print.transfer.App',{
    extend:'Shell.ux.panel.AppPanel',
    title:'选择需要打印的检验单',
    requires: [
		'Shell.ux.form.field.DateArea',
		'Shell.ux.form.field.CheckTrigger'
	],
	layout:'border',
	/**检验状态列表*/
	mainStatusList:[
	    ['-2', '作废'],
		['0', '检验中'],
		['2', '检验确认'],
		['3', '检验审核']
    ],
	/**检验状态默认值*/
    defaultMainStatus:'0',
    /**日期范围默认值*/
    defaultAddDate:null,
    /**小组*/
	SectionID: null,
	hasLoadMask:true,
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		me.onSearch();
		me.BtnPanel.on({
			addLeft : function(){//向左移
				me.Left();
			},
			addRight : function(){//向右移
				me.Right();
			},
			checked : function(){//确定
				var recs = me.Grid.store.data.items;
				me.fireEvent('checked', recs,me);
			},
			clear : function(){
				me.close();
			}
		});
		me.Grid.on({
			itemdblclick: function(grid, record) {
				me.Right();
			}
		});
		me.UnGrid.on({
			itemdblclick: function(grid, record) {
				me.Left();
			}
		});
	},
	initComponent:function(){
		var me = this;
		me.addEvents('checked');
		me.initDate();
		//创建挂靠功能栏
		me.dockedItems = me.createDockedItems();
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		var toolitems=[];
	    toolitems.push({
			xtype: 'uxdatearea',itemId: 'date',labelWidth: 60,labelAlign: 'right',
			fieldLabel: '日期范围',value:me.defaultAddDate,
			listeners: {
				enter: function() {
					me.onSearch(); 
				}
			}
		},'-',{
            xtype: 'textfield',itemId: 'StartSampleNo', fieldLabel: '样本号范围',emptyText: '开始样本号',labelAlign:'right',width:160,labelWidth:70
        },{
        	xtype: 'textfield',itemId:'EndSampleNo',emptyText: '结束样本号',fieldLabel:'-',labelSeparator:'',labelWidth:10,width:100
        }, {
		   width: 150,labelWidth: 60,labelAlign: 'right',xtype: 'uxSimpleComboBox',itemId: 'MainStatusID',
		   fieldLabel: '检验状态',data: me.mainStatusList,value: me.defaultMainStatus
	    },{text:'查询',tooltip:'查询',iconCls:'button-search',
		    handler:function(but,e){
		    	me.onSearch();
		    }
		});
		
		items.push(Ext.create('Shell.ux.toolbar.Button',{
			dock:'top',
			itemId:'bottomToolbar',
			items:toolitems
		}));
		return items;
	},
	createItems:function(){
		var me = this;
		var maxWidth = document.body.clientWidth;
		var width = Number(maxWidth/2) - 70;
		me.Grid = Ext.create('Shell.class.lts.print.transfer.Grid', {
			itemId:'Grid',
			header:false,
			region:'west',
			width:width,
			collapsible:false
		});
		me.BtnPanel = Ext.create('Shell.class.lts.print.transfer.BtnPanel', {
			itemId:'BtnPanel',
			width:70,
			region:'west',
			collapsible:false,
			split:false,
			header:false
		});
		me.UnGrid = Ext.create('Shell.class.lts.print.transfer.UnGrid', {
			region:'center',
			itemId:'UnGrid',
			header:false
		});
		return [me.Grid,me.BtnPanel,me.UnGrid];
	},
	/**初始化送检时间*/
	initDate: function() {
		var me = this;
		var Sysdate = JShell.System.Date.getDate();
		var dateArea = {
			start: Sysdate,
			end: Sysdate
		};
		me.defaultAddDate = dateArea;
	},
	onSearch : function(){
		var me = this,
		    bottomToolbar = me.getComponent('bottomToolbar'),
		    DateArea = bottomToolbar.getComponent('date'),
    	    date = DateArea.getValue(),
    	    MainStatusID = bottomToolbar.getComponent('MainStatusID').getValue(),
            StartSampleNo = bottomToolbar.getComponent('StartSampleNo').getValue(),
    	    EndSampleNo = bottomToolbar.getComponent('EndSampleNo').getValue();

        var OBJ = {
			BeginDate: JShell.Date.toString(date.start),
			EndDate: JShell.Date.toString(date.end),
			EndSampleNo:EndSampleNo,
			StartSampleNo:StartSampleNo
		};
		
        if(!DateArea.isValid()) {
        	me.hideMask();
        	return;
        }
		me.UnGrid.loadData(me.SectionID,OBJ,MainStatusID);
	},
		//向左移动数据
	Left : function(){
		var me = this;
		me.onDataOperation(me.UnGrid,me.Grid,'1');
	},
	//向右移动数据
	Right : function(){
		var me = this;
		me.onDataOperation(me.Grid,me.UnGrid,'');
	},
	/**删除选择行并向另外列表插入数据行
	 * delgrid 删除选择行列表
	 * addgrid 新增行的列表
	 * */
	onDataOperation : function(delgrid,addgrid,Tab){
		var me = this;
		var records = delgrid.getSelectionModel().getSelection();
		if(records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		Ext.Array.each(records, function(record, index, allItems) {
			var isAdd = true;
			addgrid.store.each(function(rec) {
				if(rec.get(delgrid.PKField) == record.get(addgrid.PKField)) {
					isAdd = false;
					if(!Tab)rec.set('LisTestForm_Tab',Tab);
					return false;
				}
			});
			if(isAdd == true) {
                record.data.LisTestForm_GTestDate=JShell.Date.toString(record.data.LisTestForm_GTestDate, true);
				addgrid.store.add(record.data);
			}
			var index = delgrid.store.indexOf(record);
			if(index >= 0){
				if(!Tab){
					delgrid.store.removeAt(index);
				}else{
					record.set('LisTestForm_Tab',Tab);
				}
			} 
		});
       
       //已选列表按检验日期+样本号重新排序
        me.Grid.storeSort();
		//获取已选择列表数据
	    me.UnGrid.getSelectList(me.Grid.store.data.items);
	    me.UnGrid.getView().refresh();
	}
});