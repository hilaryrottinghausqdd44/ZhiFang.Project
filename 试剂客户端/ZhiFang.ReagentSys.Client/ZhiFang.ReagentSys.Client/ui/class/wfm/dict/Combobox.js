/**
 * 字典下拉框
 * @author Jcall
 * @version 2017-03-27
 */
Ext.define('Shell.class.wfm.dict.Combobox',{
    extend:'Ext.form.field.ComboBox',
    alias:'widget.dictComboBox',
    
    /**获取数据服务路径*/
	selectUrl:'/SingleTableService.svc/ST_UDTO_SearchBDictByHQL?isPlanish=true',
	/**
	 * 排序字段
	 * @exception 
	 * [{property:'DContractPrice_ContractPrice',direction:'ASC'}]
	 */
	defaultOrderBy: [{property:'BDict_DispOrder',direction:'ASC'}],
    /**后台排序*/
	remoteSort: false,
	/**默认加载数据*/
	defaultLoad:false,
	/**类参数*/
	classConfig:{},
	
	emptyText: "--请选择--",
	editable:false,
	queryMode:'local',
	displayField:'BDict_CName',
	valueField:'BDict_Id',
	
	/**是否多选*/
	multiSelect:false,
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		if(me.defaultLoad){
			me.onSearch();
		}
	},
	initComponent:function(){
		var me = this;
	    //公开事件
	    me.addEvents('check');
	    //类参数处理
	    if(me.classConfig){
	    	for(var i in me.classConfig){
		    	me[i] = me.classConfig[i];
		    }
	    }
		
		me.store = me.createStore();
		me.callParent(arguments);
	},
	/**创建数据集*/
	createStore: function() {
		var me = this;
		return Ext.create('Ext.data.Store', {
			fields: me.getStoreFields(),
			remoteSort: me.remoteSort,
			sorters: me.defaultOrderBy,
			proxy: {
				type: 'ajax',
				url: '',
				reader: {
					type: 'json',
					totalProperty: 'count',
					root: 'list'
				},
				extractResponseData: function(response) {
					var data = JShell.Server.toJson(response.responseText);
					if (data.success) {
						var info = data.value;
						if (info) {
							data.count = info.count || 0;
							data.list = info.list || [];
						} else {
							data.count = 0;
							data.list = [];
						}
						data = me.changeResult(data);
					} else {
						data.count = 0;
						data.list = [];
					}
					response.responseText = Ext.JSON.encode(data);

					return response;
				}
			},
			listeners: {
				beforeload: function() {
					return me.onBeforeLoad();
				},
				load: function(store, records, successful) {
					me.onAfterLoad(records, successful);
				}
			}
		});
	},
	/**创建数据字段*/
	getStoreFields: function(isString) {
		var me = this;
		
		var fields = [
			{name:'BDict_CName',type:'auto'},
			{name:'BDict_Id',type:'auto'},
			{name:'BDict_DispOrder',type:'int'}
		];
		
		if(!isString){
			return fields;
		}
		
		var strFields = [];
		if(isString){
			for(var i in fields){
				strFields.push(fields[i].name);
			}
		}
		
		return strFields;
	},
	/**加载数据前*/
	onBeforeLoad: function() {
		var me = this;
		me.store.proxy.url = me.getLoadUrl(); //查询条件
	},
	/**加载数据后*/
	onAfterLoad:function(){
		var me = this;
		if(me.hasSetValue){
			
		}
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			arr = [];

		var url = JShell.System.Path.ROOT + me.selectUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');

		//默认条件
		if (me.defaultWhere && me.defaultWhere != '') {
			arr.push(me.defaultWhere);
		}
		//内部条件
		if (me.internalWhere && me.internalWhere != '') {
			arr.push(me.internalWhere);
		}
		//外部条件
		if (me.externalWhere && me.externalWhere != '') {
			arr.push(me.externalWhere);
		}
		var where = arr.join(") and (");
		if (where) where = "(" + where + ")";

		if (where) {
			url += '&where=' + JShell.String.encode(where);
		}

		return url;
	},
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		return data;
	},
	onSearch:function(){
		var me = this;
		this.store.load();
	},
	
	onListSelectionChange: function(list, selectedRecords) {
		var me = this;
		me.callParent(arguments);
		
		var checked = null;
		if(me.multiSelect){
			checked = selectedRecords;
		}else{
			checked = selectedRecords[0];
		}
		me.fireEvent('check',me,checked)
	},
	close:function(){}
});