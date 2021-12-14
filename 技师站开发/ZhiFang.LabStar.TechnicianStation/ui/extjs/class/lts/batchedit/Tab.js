/**
 * 批量处理TAB
 * @author liangyl
 * @version 2019-12-17
 */
Ext.define('Shell.class.lts.batchedit.Tab', {
	extend:'Ext.tab.Panel',
	title:'批量处理',
	defaults:{border:false},
    activeTab: 0,
    /**小组*/
	SectionID: null,
    /**已选择的检验单*/
    LisTestFormList:[],

    afterRender:function(){
		var me = this;
		me.callParent(arguments);

		me.ChoiceGrid.on({
			editClick : function(grid){//批量删除检验单（选择检验单按钮)
				me.fireEvent('editclick',grid);
			},
			itemdblclick: function(grid, record) {//双击选择行(双击选择行)
				me.fireEvent('itemdblclick',grid, record);
			}
		});
		me.Item.on({
			onSaveClick:function(){//添加检验单项目保存
				if(me.LisTestFormList.length==0){
					JShell.Msg.alert('请先选择检验单');
					return;
				}
				me.Item.onSave(me.LisTestFormList);
			},
			onDelClick:function(record){//添加检验单项目删除
				if(me.LisTestFormList.length==0){
					JShell.Msg.alert('请先选择检验单');
					return;
				}
				me.Item.onDel(me.LisTestFormList,record);
			}
		});
		me.DelItem.on({
			onDelClick: function (records) {//检验单项目删除
				if (me.LisTestFormList.length == 0) {
					JShell.Msg.alert('请先选择检验单');
					return;
				}
				me.DelItem.onDel(me.LisTestFormList, records);
			}
		});
		me.EditForm.on({
			onSaveClick : function(){//检验单修改保存
				if(me.LisTestFormList.length==0){
					JShell.Msg.alert('请先选择检验单');
					return;
				}
				me.EditForm.onSave(me.LisTestFormList);
			}
		});
		me.ManyResult.on({ 
			onSaveClick : function(){//多项目可结果保存
				if(me.LisTestFormList.length==0){
					JShell.Msg.alert('请先选择检验单');
					return;
				}
				me.ManyResult.onSave(me.LisTestFormList);
			}
		});
		me.SingleResult.on({
		 	check : function(p,record){ //单项目结果录入选择项目 
		 	  	if(me.LisTestFormList.length==0){
					JShell.Msg.alert('请先选择检验单');
					return;
				}
				me.SingleResult.onCheckItem(me.LisTestFormList,p,record);
		 	},
		 	onSaveClick : function(){//单项目结果保存
				if(me.LisTestFormList.length==0){
					JShell.Msg.alert('请先选择检验单');
					return;
				}
				me.SingleResult.onSave(me.LisTestFormList);
			}
		});
		me.Dilution.on({
			onSaveClick : function(){//稀释处理执行
				if(me.LisTestFormList.length==0){
					JShell.Msg.alert('请先选择检验单');
					return;
				}
				me.Dilution.onSave(me.LisTestFormList);
			}
		});
		me.ResultDeviation.on({
			onSaveClick : function(){//结果偏移执行
				if(me.LisTestFormList.length==0){
					JShell.Msg.alert('请先选择检验单');
					return;
				}
				me.ResultDeviation.onSave(me.LisTestFormList);
			}
		});
		
		 me.on({
			/**页签切换事件处理*/
			tabchange: function(tabPanel, newCard, oldCard, eOpts) {
				var me = this;
				switch(newCard.itemId) {
					case 'SingleResult'://单项目结果 选项目时需要查询检验单并添加项目
					    me.SingleResult.onAddItem(me.LisTestFormList);
						break;
					case 'Dilution'://稀释处理，数据查询
					    me.Dilution.loadDataByTestFormId(me.LisTestFormList);
						break;
					case 'ResultDeviation'://结果偏移，数据查询
					    me.ResultDeviation.loadDataByTestFormId(me.LisTestFormList);
						break;
					default:
						break
				}
			}
		});
	},
	initComponent:function(){
		var me = this;
		me.addEvents('editclick','itemdblclick');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.ChoiceGrid  = Ext.create('Shell.class.lts.batchedit.ChoiceGrid', {
			title: '检验单选择',
			header: true,
			border: false,
			SectionID:me.SectionID,
			itemId: 'ChoiceGrid'
		});
		me.EditForm = Ext.create('Shell.class.lts.batchedit.Form', {
			title: '检验单修改',
			header: true,
			border: false,
			itemId: 'EditForm'
		});
	    me.Item = Ext.create('Shell.class.lts.batchedit.ItemGrid', {
			title: '添加检验项目',
			header: true,
			border: false,
			SectionID:me.SectionID,
			itemId: 'ItemGrid'
		});
		me.DelItem = Ext.create('Shell.class.lts.batchedit.DelItemGrid', {
			title: '删除检验项目',
			header: true,
			border: false,
			SectionID: me.SectionID,
			itemId: 'DelItemGrid'
		});
		 me.ManyResult = Ext.create('Shell.class.lts.batchedit.ManyResult', {
			 title: '多项目结果录入',
			 header: true,
			 border: false,
			 SectionID: me.SectionID,
			 itemId: 'ManyResult'
		});
		me.SingleResult = Ext.create('Shell.class.lts.batchedit.SingleResult', {
			title: '单项目结果录入',
			header: true,
			border: false,
			SectionID: me.SectionID,
			itemId: 'SingleResult'
		});
		me.Dilution = Ext.create('Shell.class.lts.batchedit.Dilution', {
			title: '稀释处理',
			header: true,
			border: false,
			itemId: 'Dilution'
		});
		me.ResultDeviation = Ext.create('Shell.class.lts.batchedit.ResultDeviation', {
			title: '结果偏移',
			header: true,
			border: false,
			itemId: 'ResultDeviation'
		});
		
		return [me.ChoiceGrid, me.EditForm, me.Item, me.DelItem,me.ManyResult,me.SingleResult,me.Dilution,me.ResultDeviation];
	},
	//记录已选择检验单(用于颜色标记)
	getSelectList:function(list){
		var me = this;
		me.LisTestFormList = list;
		me.ChoiceGrid.getSelectList(list);
	},
	 /**返回已选择检验单的所有数据(用于颜色标记) */
	returnData:function(list){
		var me = this;
		me.LisTestFormList = list;
		me.getSelectList(list);
		me.ChoiceGrid.onSearch();
	}
});