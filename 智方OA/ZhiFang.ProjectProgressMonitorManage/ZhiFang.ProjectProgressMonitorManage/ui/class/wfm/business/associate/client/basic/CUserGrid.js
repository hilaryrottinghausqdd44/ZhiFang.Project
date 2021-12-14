/**
 * CUser 授权系统用户
 * @author liangyl
 * @version 2017-03-31
 */
Ext.define('Shell.class.wfm.business.associate.client.basic.CUserGrid', {
	extend: 'Shell.ux.grid.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '授权系统用户列表',
	/**获取数据服务路径*/
	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchCUserByHQL?isPlanish=false',
	/**修改服务地址*/
	editUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdateCUserByField',

	/**将CUser某一记录行复制到PClient中*/
	addUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_CopyCUserToPClientByCUserId',

//	defaultOrderBy: [{ property: 'CUser_DispOrder', direction: 'ASC' }],
	/**默认加载*/
	defaultLoad: true,
	/**是否启用刷新按钮*/
	hasRefresh: false,
	/**是否启用删除按钮*/
	hasDel: false,
	/**是否启用查询框*/
	hasSearch: false,
	defaultDisableControl: false,
	hiddenCheckCheckId: true,
	autoSelect: false,
	/**1,对比,2,审核;*/
    OAtype:1,
	/**是否启用序号列*/
	//hasRownumberer: false,
	initComponent: function() {
		var me = this;
		//数据列
		me.columns = me.createGridColumns();
		//创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();
		me.callParent(arguments);
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text: '区域',
			dataIndex: 'UserArea',
			width: 60,
			sortable: true,
			menuDisabled: true,
			renderer: function(value, meta, record) {
				var IsMapping = record.get("IsMapping");
				var ContrastId = record.get("ContrastId");
	            if (IsMapping=='1' || IsMapping=='true'){
					meta.style = 'background-color:#f4c600' ;
				}
	            return value;
			}
		}, {
			text: '名称',
			dataIndex: 'UserName',
			width: 160,
			sortable: true,
			menuDisabled: true,
			renderer: function(value, meta, record) {
				var IsMapping = record.get("IsMapping");
				var ContrastId = record.get("ContrastId");
	            if(IsMapping=='1' || IsMapping=='true'){
					meta.style = 'background-color:#f4c600' ;
				}
	            return value;
			}
		}, {
			xtype: 'actioncolumn',
			text: '转为新OA',
			align: 'center',
			width: 65,
			tooltip:'复制行数据到客户列表',
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					var checkId = record.get("CheckId");
					if(!checkId) {
					    return 'button-add hand';
					}
					else{
						return '';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					me.getSelectionModel().select(rowIndex);
					me.onAddSave();
				}
			}],
			renderer: function(value, meta, record) {
				var IsMapping = record.get("IsMapping");
				var ContrastId = record.get("ContrastId");
				 var checkId = record.get("CheckId");
	            if(IsMapping=='1' || IsMapping=='true'){
	            	if(!checkId) {
	            		meta.style = 'background-color:#f4c600' ;
	            	}
				}
	            return value;
			}
		},{
			text: '授权编码',
			dataIndex: 'UserFWNo',
			width: 70,
			sortable: true,
			menuDisabled: true,
			renderer: function(value, meta, record) {
				var IsMapping = record.get("IsMapping");
				var ContrastId = record.get("ContrastId");
	            if(IsMapping=='1' || IsMapping=='true'){
					meta.style = 'background-color:#f4c600' ;
				}
	            return value;
			}
		}, {
			text: '主服务器授权号',
			dataIndex: 'LRNo1',
			width: 90,
			sortable: true,
			menuDisabled: true,
			renderer: function(value, meta, record) {
				var IsMapping = record.get("IsMapping");
				var ContrastId = record.get("ContrastId");
	            if(IsMapping=='1' || IsMapping=='true'){
					meta.style = 'background-color:#f4c600' ;
				}
	            return value;
			}
		}, {
			text: '备份服务器授权号',
			dataIndex: 'LRNo2',
			width: 105,
			sortable: true,
			menuDisabled: true,
			renderer: function(value, meta, record) {
				var IsMapping = record.get("IsMapping");
				var ContrastId = record.get("ContrastId");
	            if(IsMapping=='1' || IsMapping=='true'){
					meta.style = 'background-color:#f4c600' ;
				}
	            return value;
			}
		}, {
			text: '对比人',
			dataIndex: 'ContrastCName',
			width: 55,
			sortable: false,
			menuDisabled: true,
			renderer: function(value, meta, record) {
				var v='';
				var IsMapping = record.get("IsMapping");
				var ContrastId = record.get("ContrastId");
//				if(IsMapping!='true' && IsMapping!=true && IsMapping!='1'){
	            if((ContrastId=='' || ContrastId==null) && (IsMapping=='1' || IsMapping=='true')){
					v='程序';
					meta.style = 'background-color:#f4c600' ;
				} else{
	            	if(IsMapping=='1' || IsMapping=='true'){
						meta.style = 'background-color:#f4c600' ;
					}
					v=value;
				}
	            return v;
			}
		}, {
			text: '审核人',
			dataIndex: 'CheckCName',
			width: 55,
			sortable: true,
			menuDisabled: true,
			renderer: function(value, meta, record) {
				var IsMapping = record.get("IsMapping");
				var ContrastId = record.get("ContrastId");
	            if(IsMapping=='1' || IsMapping=='true'){
					meta.style = 'background-color:#f4c600' ;
				}
	            return value;
			}
		},{
			text:'主键ID',dataIndex:'Id',
			isKey:true,hidden:true,hideable:false
		},{
			text:'IsMapping',dataIndex:'IsMapping',
			hidden:true,hideable:false
		},{
			text:'ContrastId',dataIndex:'ContrastId',
			hidden:true,hideable:false
		},{
			text:'CheckId',dataIndex:'CheckId',
			hidden:true,hideable:false
		}];

		return columns;
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems: function() {
		var me = this,
			buttonToolbarItems = me.buttonToolbarItems || [];
        buttonToolbarItems.push('refresh', '-');
		buttonToolbarItems.push( {
//			xtype: 'textfield',
			itemId: 'ProvinceID',
			width:110,
			labelWidth: 0,
			emptyText: '省份',
			fieldLabel: '',
			labelSeparator: '',
			hidden: false,
			xtype:'trigger',
			triggerCls:'x-form-search-trigger',
			enableKeyEvents:true,
			onTriggerClick:function(){
//				if(me.ownerCt['onSearchClick']){
//					me.ownerCt['onSearchClick'](me,this.getValue());
//				}
			},
			listeners:{
	            keyup:{
	                fn:function(field,e){
	                	if(e.getKey() == Ext.EventObject.ESC){
	                		field.setValue('');
//	                		if(me.ownerCt['onSearchClick']){
//								me.ownerCt['onSearchClick'](me,field.getValue());
//							}
	                	}else if(e.getKey() == Ext.EventObject.ENTER){
							me.onSearch();
	                	}
	                }
	            }
	        }
		}, {
			boxLabel: '包含已对应',
			name: 'isCheck',
			itemId: 'IsMapping',
			xtype: 'checkbox',
			checked: false,
			value: false,
			fieldLabel: '&nbsp;',
			labelSeparator: '',
			labelWidth: 0,
//			height:20,
			width: 95,
			style: {
				marginLeft: '5px'
			},
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.onSearch();
				}
			}
		}, {
		    boxLabel: '包含已审核',
			name: 'isCheckCheckId',
			itemId: 'isCheckCheckId',
			xtype: 'checkbox',
			checked: false,
			value: false,
			fieldLabel: '&nbsp;',
			labelSeparator: '',
			labelWidth: 0,
			width: 110,
//			height:20,
			hidden: me.hiddenCheckCheckId,
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.onSearch();
				}
			}
		});
		//查询框信息
		me.searchInfo = {
			width: 125,
			emptyText: '用户名称',
			isLike: true,
			itemId: 'search',
			fields: ['cuser.UserName']
		};
		buttonToolbarItems.push('->', {
			type: 'search',
			info: me.searchInfo
		});
		return buttonToolbarItems;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			ProvinceID = null,
			IsMapping = false,
//			isCheckPayOrg = false,
			isCheckCheckId = false,
			search = null,
			params = [];
		me.internalWhere = '';
		var buttonsToolbar = me.getComponent('buttonsToolbar');

		if(buttonsToolbar) {
			ProvinceID = buttonsToolbar.getComponent('ProvinceID').getValue();
			IsMapping = buttonsToolbar.getComponent('IsMapping').getValue();
			isCheckCheckId = buttonsToolbar.getComponent('isCheckCheckId').getValue();
			search = buttonsToolbar.getComponent('search').getValue();
			
		}
		if(ProvinceID){
			params.push("cuser.UserArea='"+ProvinceID+"'");
		}
		var tempWhere = " 1=1 ";
		if (IsMapping == false) {
		    tempWhere += " and (cuser.IsMapping=0 or cuser.IsMapping=null)";
		}
		
		if (me.hiddenCheckCheckId == false) {
		    if (isCheckCheckId == true) {
		        tempWhere += ' and ( cuser.CheckId is null or cuser.CheckId is not null)';
		    } else {
		        tempWhere += ' and cuser.CheckId is null';
		    }
		}
		if(tempWhere.length > 0) {
			//tempWhere = tempWhere.substring(3, tempWhere.length);
			params.push("(" + tempWhere + ")");
		}
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
	/**@overwrite 复制行处理方法*/
	onAddSave: function() {
		var me = this;
		var isSave = true;
		var msg = "";
        var Id="";
		var records = me.getSelectionModel().getSelection();
		var record = null;
		
		if(!records || records.length != 1) {
			isSave = false;
			if (records.length == 0) {
				JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			}
			return;
		} else {
			record = records[0];
		}
		
		if(record.get("CheckId")){
			isSave = false;
			JShell.Msg.error("当前选择行已经被审核!");
			return;
		}
		Id=record.get(me.PKField);
		if(isSave == true && Id) {
			me.showMask("数据提交保存中...");
			var url = (me.addUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.addUrl;
			url += (url.indexOf('?') == -1 ? '?' : '&') + 'id=' + Id+'&type='+me.OAtype;
			JShell.Server.get(url, function(data) {
				if(data.success) {
					me.hideMask();
					me.fireEvent('onAddPClientClick', me,record,me.OAtype);
				} else {
					JShell.Msg.error('将CUser某一记录行复制到PClient：' + data.msg);
				}
			});
		}
	}
});