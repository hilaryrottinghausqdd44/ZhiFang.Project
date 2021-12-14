/**
 * 批量修改
 * @author liangyl	
 * @version 2019-12-17
 * 已选检验单列表除删除外不根据选中行操作，取列表内全部数据
 */
Ext.define('Shell.class.lts.batchedit.App',{
    extend:'Shell.ux.panel.AppPanel',
    title:'批量修改',
    hasLoadMask:true,
    /**小组*/
	SectionID: null,
	
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
        me.Btn.on({
			left : function(){//向左移
				me.addLeft();
			},
			right : function(){//向右移
				me.addRight();
			}
		});
		me.Tab.on({
			itemdblclick: function(grid, record) {//双击选择样本单
				me.addLeft();
			},
			editclick : function(grid){ //样本单删除按钮
				me.Grid.onEditClick();
			}
		});
		me.Grid.on({
			itemdblclick: function(grid, record) {
				me.addRight();
			},
			save : function(){//批量删除成功后处理	
                //获取已选择列表数据()
				me.Tab.returnData(me.Grid.store.data.items);
			}
		});
	},
	initComponent:function(){
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	
	createItems:function(){
		var me = this;
		
		me.Grid = Ext.create('Shell.class.lts.batchedit.Grid', {
			region:'west',
			width:330,
			itemId:'Grid',
			split:true,
			collapsible:false
		});
		me.Btn = Ext.create('Shell.class.lts.batchedit.Btn', {
			itemId:'Btn',
			width:70,
			region:'west',
			collapsible:false,
			split:true,
			header:false
		});
		me.Tab = Ext.create('Shell.class.lts.batchedit.Tab', {
			region:'center',
			itemId:'Tab',
		    SectionID:me.SectionID,
			header:false
		});
		
		return [me.Grid,me.Btn,me.Tab];
	},
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		if (me.hasLoadMask) {
			me.body.mask(text);
		} //显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if (me.hasLoadMask) {
			me.body.unmask();
		} //隐藏遮罩层
	},
	//向左移动数据
	addLeft : function(){
		var me = this;
		me.onDataOperation(me.Tab.ChoiceGrid,me.Grid,'1');
		me.Tab.ChoiceGrid.getSelectionModel().deselectAll();
	},
	//向右移动数据
	addRight : function(){
		var me = this;
		me.Tab.setActiveTab(me.Tab.ChoiceGrid);
		me.onDataOperation(me.Grid,me.Tab.ChoiceGrid,'');
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
	    me.Grid.getSelectionModel().selectAll();
	    me.Tab.getSelectList(me.Grid.store.data.items);
	}
});