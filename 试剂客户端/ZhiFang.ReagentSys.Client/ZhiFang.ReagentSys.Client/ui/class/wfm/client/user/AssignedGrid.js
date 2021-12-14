/**
 * 已分配客户列表
 * @author liangyl
 * @version 2017-5-10
 */
Ext.define('Shell.class.wfm.client.user.AssignedGrid',{
    extend:'Shell.class.wfm.client.user.Grid',
	/**已分配客户*/
	OnlyClient:true,
	
	//复选框
	multiSelect: true,
	selType: 'checkboxmodel',
	//只能点击复选框才能选中
	selModel: new Ext.selection.CheckboxModel({checkOnly:true}),
	
	/**新增、删除数据信息*/
	errorList:[],
	addRecords:[],
	delRecords:[],
	addCount:0,
	delCount:0,
	/**销售人员ID*/
	SalesManID:null,
	/**销售人员姓名*/
	SalesManName:null,
	/**显示可撤销批量撤销*/
    IsAdmin:false,
    afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			load:function(){
				var save=me.getComponent('buttonsToolbar').getComponent('save');
			    var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID)+'';
		        //管理员有分下来的权限
				if(userId == me.SalesManID && me.IsAdmin!=true){
				    save.disable();
				}else{
					save.enable()
				}
			}
		});
    },
		/**创建功能按钮栏Items*/
	createButtonToolbarItems:function(){
		var me = this,
			buttonToolbarItems =me.callParent(arguments);
		buttonToolbarItems.push('-',{
			text:'打勾批量撤销',
			iconCls:'button-save',
			itemId:'save',
			tooltip:'打勾批量撤销客户',
			handler:function(){
				me.onSaveClick();
			}
		},{
			text:'导出',iconCls:'file-excel',itemId:'Excel',tooltip:'导出', 
			handler:function(){
				me.onDownExcel();
			}
		});
		return buttonToolbarItems;
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = me.callParent(arguments);
		columns.splice(2,0, {
			xtype: 'actioncolumn',
			text: '单个撤销',
			align: 'center',
			width: 65,
			hideable: false,
			sortable: false,
			menuDisabled: true,
			style: 'font-weight:bold;color:white;background:orange;',
			items: [{
			    getClass: function(v, meta, record) {
			    	 var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID)+'';
		            //管理员有分下来的权限
					if(userId == me.SalesManID && me.IsAdmin!=true){
					    return 'button-actionedit hand';
					}else{
						return 'button-edit hand';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					me.delCount=0;
					var rec = grid.getStore().getAt(rowIndex);
				    var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID)+'';
				    
			        //管理员有分下来的权限
					if(userId!= me.SalesManID || ( userId == me.SalesManID  && me.IsAdmin)){
					    me.onOneSaveClick(rec);
					}
				}
			}]
		});
		return columns;
	},
	/**保存*/
	onOneSaveClick:function(rec){
		var me = this;
		me.delRecords=[];
		var PSalesManClientLinkID = rec.get('PClient_PSalesManClientLinkID');
		if(PSalesManClientLinkID){
	        me.delRecords.push(rec);
		}
		if( me.delRecords.length > 0){
			me.onAddAndDel();//删除数据
		}
	},
	/**保存*/
	onSaveClick:function(){
		var me = this,
			records = me.store.data.items,
			selectRecords = me.getSelectionModel().getSelection();
		
		//清空新增、删除列表
		me.errorList =[];
		me.addRecords = [];
		me.delRecords = [];
		me.addCount = 0;
		me.delCount = 0;

		var recordsLen = records.length;
		for(var i=0;i<recordsLen;i++){
			var PSalesManClientLinkID = records[i].get('PClient_PSalesManClientLinkID');
			if(PSalesManClientLinkID){
				var isInArray = me.isInSelectRecords(selectRecords,PSalesManClientLinkID);
				if(isInArray){
					me.delRecords.push(records[i]);
				}
			}
		}
		
		if( me.delRecords.length > 0){
			me.onAddAndDel();//新增、删除数据
		}
	},

	/**删除一条数据*/
	onDelOne: function(record) {
		var me = this,
			index = me.store.indexOfTotal(record),
			id = record.get('PClient_PSalesManClientLinkID'),
			url = JShell.System.Path.getRootUrl(me.delUrl) + '?id=' + id;

		JShell.Server.get(url, function(data) {
			me.delCount++;
			if (!data.success) {
				var info = '【序号：' + (index + 1) + '，删除错误！】' + record.get('PClient_Name');
				me.errorList.push(info);
			}else{
				record.set('PClient_PSalesManClientLinkID','');
				record.commit();
			}
			me.onSaveOver();//保存结束
		});
	},
	ShowText:function(SalesManName,SalesManID,IsAdmin){
		var me=this,IsExect=true;
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID)+'';
		var	buttonsToolbar = me.getComponent('buttonsToolbar');
		var	ShowText = buttonsToolbar.getComponent('ShowText');
        ShowText.setText(SalesManName+'所负责的用户');
	}
	
});
	