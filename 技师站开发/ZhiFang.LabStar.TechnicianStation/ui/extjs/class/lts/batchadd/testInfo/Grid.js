/**
 * 检验评语列表
 * @author liangyl
 * @version 2019-12-26
 */
Ext.define('Shell.class.lts.batchadd.testInfo.Grid', {
	extend: 'Shell.ux.grid.Panel',
	requires: ['Ext.ux.CheckColumn'],
	title: '检验评语列表',
	
    /**获取数据服务路径*/
	selectUrl:'/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBPhraseByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl:'/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_UpdateLBPhraseByField',
	/**删除数据服务路径*/
	delUrl:'/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DelLBPhrase',
    /**新增数据服务路径*/
	addUrl:'/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddLBPhrase',

	width: 800,
	height: 500,
	/**显示成功信息*/
	showSuccessInfo: false,
	/**消息框消失时间*/
	hideTimes: 3000,
	/**默认加载*/
	defaultLoad: true,
	/**默认每页数量*/
	defaultPageSize:1000,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**后台排序*/
	remoteSort: false,
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**带功能按钮栏*/
	hasButtontoolbar:true,
	/**是否启用序号列*/
	hasRownumberer: true,
	//排序字段
	defaultOrderBy: [{property:'LBPhrase_DispOrder',direction:'ASC'}],
	  /**小组*/
	SectionID: 1,
	  /**检验评语*/
	TypeName:'检验评语',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			nodata:function(){
				me.getView().update('');
			},
			beforeclose : function ( panel,eOpts ){
				me.cancelEdit();
			}
		});
	},
	
	initComponent: function() {
		var me = this;
		
		me.addEvents('onAcceptClick');
		
		me.searchInfo = {width: 165,emptyText: '短语名称/快捷码',itemId: 'search',isLike: true,fields: ['lbphrase.CName','lbphrase.Shortcode']};
		
		//创建功能按钮栏Items
		me.buttonToolbarItems = ['refresh','add','del','save','-',{
			type: 'search',
			info: me.searchInfo
		},'->',{ xtype: "checkbox",boxLabel: "选择后关闭",inputValue: "true",checked: true,
			itemId: "IsClose",name: "IsClose",fieldLabel: ""
		},'accept'];
		
		//数据列
		me.columns = me.createGridColumns();
		
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1,
			pluginId:'NewsGridEditing'
		});
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){		  
		var me = this;
		var columns = [{
			text:'短语ID',dataIndex:'LBPhrase_Id',isKey:true,hidden:true,hideable:false
		},{
			text:'短语名称',dataIndex:'LBPhrase_CName',width:175,
			sortable:false,menuDisabled:true,
			editor: {xtype: 'textfield',allowBlank: false},defaultRenderer:true
		},{
			text:'快捷码',dataIndex:'LBPhrase_Shortcode',width:100,
			sortable:false,menuDisabled:true,editor: {xtype: 'textfield'},defaultRenderer:true
		},{
			text:'显示次序',dataIndex:'LBPhrase_DispOrder',width:80,
			sortable:false,menuDisabled:true,
			editor: {xtype:'numberfield',value:0,allowBlank: false},defaultRenderer:true
		},{
			xtype: 'actioncolumn',text: '选择',align: 'center',width: 45,
			style:'font-weight:bold;color:white;background:orange;',hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					var id = record.get(me.PKField);
					if(!id) {
					    return '';
					}else{
						return 'button-accept hand';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var IsClose = me.getComponent('buttonsToolbar').getComponent('IsClose');
					me.fireEvent('onAcceptClick',rec,IsClose.getValue());
				}
			}]
		}];
		return columns;
	},
	/**查询数据*/
	onSearch: function(autoSelect) {
		var me = this;
		//小组Id
		if(!me.SectionID) {
			JShell.Msg.error("小组ID不能为空");
			return;	
		}
		me.load(null, true, autoSelect);
	},
	/**增加一行*/
	createAddRec: function(num,count) {
		var me = this;
		var obj = {
			LBPhrase_Id:'',
			LBPhrase_CName:'',
			LBPhrase_Shortcode:'',
			LBPhrase_DispOrder:num
		};
		me.store.insert(count, obj);
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick:function(){
		var me = this;
		me.createAddRec(me.getStore().getCount()+1,me.getStore().getCount());
	},
	/**保存勾选数据*/
	onSaveClick:function(){
		var me = this,
			records=me.store.getModifiedRecords(),//获取修改过的行记录
			len = records.length;
			
		if(len == 0) return;
		var IsValid = me.IsValid();
		if(!IsValid)return;
		me.showMask(me.saveText);//显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;
		
		for(var i=0;i<len;i++){
			var rec = records[i];
			var id = rec.get(me.PKField);
	
			if(id){//修改
				me.updateOne(rec,id);
			}else{
				//新增
				me.addOne(rec,id);
			}
		}
	},
		
	getEntity : function(rec,id){
		var me = this;
		var entity = {
			Shortcode: rec.get('LBPhrase_Shortcode'),
            CName:rec.get('LBPhrase_CName'),
            IsUse:1,
            ObjectType:1,
            PhraseType:'SamplePhrase',
            TypeName:me.TypeName
		};
		entity.ObjectID = me.SectionID;
		if(id)entity.Id = rec.get('LBPhrase_Id');
		if(rec.get('LBPhrase_DispOrder'))entity.DispOrder=rec.get('LBPhrase_DispOrder');
		return entity;
	},
	/**添加一条关系*/
	addOne:function(rec,id){
		var me = this;
		var url = JShell.System.Path.ROOT + me.addUrl;
		var entity = me.getEntity(rec,id);
		JShell.Server.post(url,Ext.JSON.encode({entity:entity}),function(data){
			if(data.success){
				me.saveCount++;
				rec.commit();
			}else{
				me.saveErrorCount++;
			}
			me.onSaveEnd();
		});
	},
	/**更新一条数据*/
	updateOne:function(rec,id){
		var me = this;
		var url = JShell.System.Path.ROOT + me.editUrl;
		var entity = me.getEntity(rec,id);
			
		var fields ='Id,Shortcode,CName,DispOrder';
		JShell.Server.post(url,Ext.JSON.encode({entity:entity,fields:fields}),function(data){
			if(data.success){
				me.saveCount++;
				rec.commit();
			}else{
				me.saveErrorCount++;
			}
			me.onSaveEnd();
		});
	},
	onSaveEnd:function(){
		var me = this;
		if (me.saveCount + me.saveErrorCount == me.saveLength) {
			me.hideMask(); //隐藏遮罩层
			if (me.saveErrorCount == 0){
				JShell.Msg.alert(JShell.All.SUCCESS_TEXT,null,1000);
				me.onSearch();
			}else{
				JShell.Msg.error('存在失败信息，请重新保存！');
			}
		}
	},
	/**删除按钮点击处理方法*/
	onDelClick: function() {
		var me = this,
			records = me.getSelectionModel().getSelection();
		
		if (records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		JShell.Msg.del(function(but) {
			if (but != "ok") return;

			me.delErrorCount = 0;
			me.delCount = 0;
			me.delLength = records.length;

			me.showMask(me.delText); //显示遮罩层
			
			for (var i in records) {
				var id = records[i].get(me.PKField);
				if(!id){
					if (records[i]) {
						records[i].set(me.DelField, true);
						me.store.remove(records[i]);
						records[i].commit();
					}
					me.delCount++;
					if (me.delCount + me.delErrorCount == me.delLength) {
						me.hideMask(); //隐藏遮罩层
						if (me.delErrorCount == 0){
							me.onSearch();
						}else{
							JShell.Msg.error('存在失败信息，具体错误内容请查看数据行的失败提示！');
						}
					}
				}else{
					me.delOneById(i, id,records[i]);
				}
			}
		});
	},
	/**删除一条数据*/
	delOneById: function(index, id) {
		var me = this;
		var url = (me.delUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.delUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'id=' + id;

		setTimeout(function() {
			JShell.Server.get(url, function(data) {
				var record = me.store.findRecord(me.PKField, id);
				if (data.success) {
					if (record) {
						record.set(me.DelField, true);
						me.store.remove(record);
						record.commit();
					}
					me.delCount++;
				} else {
					me.delErrorCount++;
					if (record) {
						record.set(me.DelField, false);
						record.set('ErrorInfo', data.msg);
						record.commit();
					}
				}
				if (me.delCount + me.delErrorCount == me.delLength) {
					me.hideMask(); //隐藏遮罩层
					if (me.delErrorCount == 0){
						me.onSearch();
					}else{
						JShell.Msg.error('存在失败信息，具体错误内容请查看数据行的失败提示！');
					}
				}
			});
		}, 100 * index);
	},
	/**数据校验*/
	IsValid: function() {
		var me = this,
			records = me.store.getModifiedRecords(),
			len = records.length,
			isExec = true;

		for(var i=0;i<len;i++){
			var Name =records[i].get('LBPhrase_CName');
			if(Name)Name.replace(/^\s\s*/, '' ).replace(/\s\s*$/, '' );
			if(!Name) {
				JShell.Msg.error('短语名称不能为空!');
				isExec = false;
				return;
			}
		}	
		return isExec;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			search = null,
			params = [];
			
		//小组Id
		if(me.SectionID) {
			params.push("lbphrase.ObjectID=" + me.SectionID + "");
		}
		if(me.TypeName){
			params.push("lbphrase.TypeName='" + me.TypeName + "'");
		}
		params.push("lbphrase.ObjectType=1");
		
		search = me.getComponent('buttonsToolbar').getComponent('search').getValue();
		
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		
		if(search) {
			if(me.internalWhere) {
				me.internalWhere += ' and (' + me.getSearchWhere(search) + ')';
			} else {
				me.internalWhere = me.getSearchWhere(search);
			}
		}
		return me.callParent(arguments);
	},
	/**@overwrite 重写确定按钮*/
	onAcceptClick: function() {
		var me = this;
		var IsClose = me.getComponent('buttonsToolbar').getComponent('IsClose');
		me.fireEvent('onAcceptClick',null,IsClose.getValue());
//		me.fireEvent('onAcceptClick',me);
	},
	cancelEdit:function(){
		var me = this;
		var edit = me.getPlugin('NewsGridEditing');  
        edit.cancelEdit();  
	}
});