/**
 * 合并项目列表
 * @author liangyl	
 * @version 2019-11-26
 */
Ext.define('Shell.class.lts.itemmerge.ItemGrid', {
    extend: 'Shell.ux.grid.PostPanel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.BoolComboBox'
	],
	title: '合并项目列表',
	width: 300,
	height: 400,
	selectUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_QueryItemMergeInfo',
	/**默认加载数据*/
	defaultLoad: false,
	/**默认选中数据*/
	autoSelect: true,
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**带功能按钮栏*/
	hasButtontoolbar: false,
	/**组合项目ID*/
	GroupItemID:null,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			nodata:function(){
				me.getView().update('');
			},
			beforeclose : function ( panel,eOpts ){
				var edit = panel.getPlugin('NewsGridEditing');  
                edit.cancelEdit();  
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.addEvents('cellValChange');
		//创建数据列
		me.columns = me.createGridColumns();
        me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1,
			pluginId: 'NewsGridEditing'
		});
		me.callParent(arguments);
	},

	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [
           {menuDisabled: false, text: '转化项目', dataIndex: 'LBMergeItemVO_ChangeItemName',sortable: false,
			width:150,editor: {
				xtype: 'uxCheckTrigger',
				allowBlank: false,
				blankText: "转化项目不能为空",
				className: 'Shell.class.lts.itemmerge.CheckGrid',
				classConfig: {
					title: '转化项目选择',
					checkOne: true,
					width:400,
					height:200,
					hasClearButton: false,
					hasAcceptButton: false,
					hasPagingtoolbar: false,
					hasButtontoolbar: false
				},
				listeners: {
					beforetriggerclick:function(com){
						com.classConfig.defaultWhere="GroupItemID="+ me.GroupItemID;
					},
					check: function(p, record) {
						var bo = true;
						var records = me.getSelectionModel().getSelection();
						if(records.length != 1) {
							JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
							return;
						}
						var CName = record ? record.get('LBItemGroup_LBItem_CName') : '';
						var LBItemId = record ? record.get('LBItemGroup_LBItem_Id') : '';
						var items = me.store.data.items;
						for(var i = 0; i < items.length; i++) { //从节点中取出子节点依次遍历
							if(CName == items[i].data.LBMergeItemVO_ChangeItemName && LBItemId == items[i].data.LBMergeItemVO_ChangeItemID ) {
								JShell.Msg.alert('该转化项目已存在,请重新选择!');
								bo = false;
								return;
							}
						}
						if(bo == true) {
							records[0].set('LBMergeItemVO_ChangeItemID', record ? record.get('LBItemGroup_LBItem_Id') : '');
							records[0].set('LBMergeItemVO_ChangeItemName', record ? record.get('LBItemGroup_LBItem_CName') : '');
							records[0].set('LBMergeItemVO_ChangeItemDispOrder', record ? record.get('LBItemGroup_LBItem_DispOrder') : '');
							me.getView().refresh();
							//更新列表
							var arr = [];//转化后的数据
							me.store.data.each(function (record) { arr.push(record["data"]); });
							arr = arr.sort(me.compare('LBMergeItemVO_ChangeItemDispOrder'));
							me.store.loadData(arr);

							me.fireEvent('cellValChange', me);
						}
						p.close();
					}
				}
			},
			defaultRenderer: true
        },{
            menuDisabled: false, text: '转化项目ID', dataIndex: 'LBMergeItemVO_ChangeItemID',sortable: false,hidden:true,
			width:80,defaultRenderer: true
        },{
            menuDisabled: false, text: '组合ID', dataIndex: 'LBMergeItemVO_ParItemID',sortable: false,hidden:true,
			width:80,defaultRenderer: true
        },{
            menuDisabled: false, text: '组合', dataIndex: 'LBMergeItemVO_ParItemName',sortable: false,
			width:80,defaultRenderer: true
        },{
            menuDisabled: false, text: '单项ID', dataIndex: 'LBMergeItemVO_LisTestItem_LBItem_Id',sortable: false,
			hidden:true,width:150,defaultRenderer: true
        },{
            menuDisabled: false, text: '单项', dataIndex: 'LBMergeItemVO_LisTestItem_LBItem_CName',sortable: false,
			width:150,defaultRenderer: true
        },{
            menuDisabled: false, text: '样本号', dataIndex: 'LBMergeItemVO_LisTestItem_LisTestForm_GSampleNo',sortable: false,
			width:80,defaultRenderer: true
        },{
            menuDisabled: false, text: '检验单项目ID', dataIndex: 'LBMergeItemVO_LisTestItem_Id',sortable: false,
			hidden:true,width:80,defaultRenderer: true
        },{
            menuDisabled: false, text: '项目结果', dataIndex: 'LBMergeItemVO_LisTestItem_ReportValue',sortable: false,
			width:80,defaultRenderer: true
        },{
            menuDisabled: false, text: '合并', dataIndex: 'LBMergeItemVO_IsMerge',sortable: false,
			width:50,editor:{xtype:'uxBoolComboBox',value:true,hasStyle:true,
				 listeners: {
				 	change : function (com,newValue,oldValue,eOpts ){
				 		var records = me.getSelectionModel().getSelection();
						if(records.length != 1) {
							JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
							return;
						}
				 		me.setIsMerge(records[0].get('LBMergeItemVO_LisTestItem_Id'),newValue);
				 	}
				 }
			},isBool: true,
			align: 'center',type: 'bool',defaultRenderer: true
        },{
            menuDisabled: false, text: '时间', dataIndex: 'LBMergeItemVO_LisTestItem_TestTime',sortable: false,
			width:80,defaultRenderer: true
        },{
            menuDisabled: false, text: '核收日期', dataIndex: 'LBMergeItemVO_LisTestItem_ReceiveTime',sortable: false,
			width:135,defaultRenderer: true
        },{
            menuDisabled: false, text: '检验单ID', dataIndex: 'LBMergeItemVO_LisTestItem_LisTestForm_Id',sortable: false,
			hidden:true,width:80,defaultRenderer: true
        },{
            menuDisabled: false, text: '检验单主状态', dataIndex: 'LBMergeItemVO_LisTestItem_LisTestForm_MainStatusID',sortable: false,
			hidden:true,width:80,defaultRenderer: true
        },{
            menuDisabled: false, text: '病历号', dataIndex: 'LBMergeItemVO_LisTestItem_LisTestForm_PatNo',sortable: false,
			hidden:true,width:80,hidden:true,defaultRenderer: true
        },{
            menuDisabled: false, text: 'EquipID', dataIndex: 'LBMergeItemVO_LisTestItem_EquipID',sortable: false,
			hidden:true,width:80,hidden:true,defaultRenderer: true
        },{
			menuDisabled: false, text: '排序字段', dataIndex: 'LBMergeItemVO_ChangeItemDispOrder',sortable: false,
			hidden:true,width:80,hidden:true,defaultRenderer: true
        }];
		return columns;
	},
	setIsMerge:function(id,value){
		var me=this;
		me.store.each(function(record) {
			if(record.get('LBMergeItemVO_LisTestItem_Id') == id ){
				record.set('LBMergeItemVO_IsMerge',value);
				me.fireEvent('cellValChange', me);
			}else{
				record.set('LBMergeItemVO_IsMerge',false);
			}
        });
        me.getView().refresh();
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function(obj) {
		var me = this;
		me.doFilterParams(obj);
		return me.callParent(arguments);
	},
	/**@overwrite 条件处理*/
	doFilterParams: function(obj) {
		var me = this,
			params = me.params || {},
			ParaClass = {};
		me.postParams = {
			itemID:obj.itemID,
			patNo:obj.PatNo,
			cName:obj.CName,
			beginDate: obj.beginDate,
			endDate:obj.endDate,
			isPlanish: true,
			isMerge:obj.isMerge,
			fields: me.getStoreFields(true).join(',')
		};
	},
	/**查询数据*/
	onSearch:function(obj){
		var me = this,
			collapsed = me.getCollapsed();
		me.GroupItemID = obj.itemID;
		me.defaultLoad = true;
		
		//收缩的面板不加载数据,展开时再加载，避免加载无效数据
		if(collapsed){
			me.isCollapsed = true;
			return;
		}
		
		me.disableControl();//禁用 所有的操作功能
		me.showMask(me.loadingText);//显示遮罩层
		var url = me.getLoadUrl(obj);
		var params = Ext.JSON.encode(me.postParams);
		JShell.Server.post(url,params,function(data){
			me.hideMask();//隐藏遮罩层
			me.fireEvent("ItemGridAfterLoad",me);
			if(data.success){
				var obj = data.value || {};
				var list = obj.list || [];
				me.store.loadData(list);
				
				if(list.length == 0){
					var msg = me.msgFormat.replace(/{msg}/,JShell.Server.NO_DATA);
					JShell.Action.delay(function(){me.getView().update(msg);},200);
				}else{
					me.fireEvent('changeResult', me, data);
					if(me.autoSelect){
						me.doAutoSelect(list,true);
					}
				}
			}else{
				var msg = me.errorFormat.replace(/{msg}/,data.msg);
				JShell.Action.delay(function(){me.getView().update(msg);},200);
			}
		});
	},
	//用于排序
	compare:function (arg) {
		return function (a, b) {
			return a[arg] - b[arg];
		}
	}
});