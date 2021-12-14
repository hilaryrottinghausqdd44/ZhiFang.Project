/**
 * 仪器模板列表
 * @author liangyl
 * @version 2016-08-12
 */
Ext.define('Shell.class.qms.equip.templet.Grid', {
	extend: 'Shell.class.qms.equip.templet.basic.Grid',
	title: '仪器模板列表',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.BoolComboBox'
	],
	selectUrl: '/QMSReport.svc/QMS_UDTO_GetETempletByHRDeptID?isPlanish=true',
	/**删除仪器Excel模板及相关数据*/
	delTempletDataUrl:'/QMSReport.svc/QMS_UDTO_DelETemplet',
	IsTrue: false,
	/**默认加载数据*/
	defaultLoad: false,
	BDictTreeId: '',
	hasDelTempletData:true,
	/**默认排序字段*/
	defaultOrderBy: [{property: 'ETemplet_DispOrder',direction: 'ASC'}],
	/**后台排序*/
	remoteSort: false,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			itemdblclick:function(view,record){
				var ETempletId=record.get('ETemplet_Id');
				var SectionID=record.get('ETemplet_Section_Id');
				me.onShowEditForm(ETempletId,SectionID);
			}
		});
	},
    createButtonToolbarItems:function(){
    	var me=this;
    	var buttonToolbarItems=me.callParent(arguments);
    	buttonToolbarItems.splice(1,0,'-','add',{
			text:'重新上传模板',tooltip:'重新上传模板',
			iconCls:'button-edit',
			handler: function() {
				var records = me.getSelectionModel().getSelection();
				if(records.length != 1) {
					JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
					return;
				}
				var ETempletId=records[0].get('ETemplet_Id');
				var SectionID=records[0].get('ETemplet_Section_Id');
				me.EditClick(ETempletId,SectionID);
			}
		},{
			text:'修改模板信息',tooltip:'修改模板信息',
			iconCls:'button-edit',
			handler: function() {
				var records = me.getSelectionModel().getSelection();
				if(records.length != 1) {
					JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
					return;
				}
				var ETempletId=records[0].get('ETemplet_Id');
				var SectionID=records[0].get('ETemplet_Section_Id');
				me.onShowEditForm(ETempletId,SectionID);
			}
		},'del');
    	
    	
    	var ACCOUNTNAME=JShell.System.Cookie.get(JShell.System.Cookie.map.ACCOUNTNAME);
		if(ACCOUNTNAME=='admin'){
			buttonToolbarItems.splice(3,0,{
				text:'删除模板及相关数据',tooltip:'删除模板及相关数据',
				iconCls:'button-del',
				handler: function() {
					me.onSaveDelTempletData();
				}
			});
		}
    	return buttonToolbarItems;
    },
	/**创建数据列*/
	createGridColumns: function() {
		var me = this,
			columns = me.callParent(arguments);
		columns.push({
			text: '小组',dataIndex: 'ETemplet_Section_Id',width: 120,
			hidden:true,sortable: false,defaultRenderer: true
		},{
			text: '仪器',dataIndex: 'ETemplet_EEquip_CName',width: 120,
			sortable: true,defaultRenderer: true
		},{
			text: '仪器id',dataIndex: 'ETemplet_Equip_Id',width: 150,
			sortable: false,defaultRenderer: true,hidden: true
		},{
			text: '类型id',dataIndex: 'ETemplet_TempletType_Id',
			width: 120,hidden:true,sortable: false,defaultRenderer: true
		},{
			text: '类型',dataIndex: 'ETemplet_TempletType_CName',
			width: 120,sortable: true,defaultRenderer: true
		},{
			text:'审核类型',dataIndex:'ETemplet_CheckType',width:70,
			align: 'center',sortable:true,
			renderer : function(value, meta) {
				var v = value + '';
				if (v == '1') {
					v ='按天审核';
				} else if (v == '0') {
					v = '按月审核';
				} else {
					v == '';
				}

				return v;
			}
		},{
			text: '小组',dataIndex: 'ETemplet_Section_CName',
			width: 120,sortable: true,defaultRenderer: true
		},{
			text: '代码',dataIndex: 'ETemplet_UseCode',width: 80,
			hidden: false,sortable: true,defaultRenderer: true
		}, {
			xtype: 'actioncolumn',text: '预览',align: 'center',
			tooltip: '预览',width: 40,style: 'font-weight:bold;color:white;background:orange;',hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					return 'button-show hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					me.getSelectionModel().select(rowIndex);
					var records = me.getSelectionModel().getSelection();
					if(records.length != 1) {
						JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
						return;
					}
					var PreviewPdfUrl = me.getUrl(records[0]);
					me.openPdfForm(true, false, PreviewPdfUrl);
				}
			}]
		}, {
			xtype: 'actioncolumn',text: '下载',align: 'center',tooltip: '下载',width: 40,
			style: 'font-weight:bold;color:white;background:orange;',hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					return 'button-exp hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var ETempletId = rec.get('ETemplet_Id');
					me.downExcelTemplet(ETempletId);
				}
			}]
		}, {
			text: '填写说明',dataIndex: 'ETemplet_Comment',width: 180,
			hidden: false,sortable: true,
			renderer: function(value, meta, record) {
            	var v=me.showMemoText(value, meta);
				return v;
			}
		},{
			text: '模板新增时间',dataIndex: 'ETemplet_DataAddTime',
			width: 135,isDate: true,hasTime: true,hidden:true,sortable: true,
			menuDisabled: true,defaultRenderer: true
		},{
			text: '模板更新时间',dataIndex: 'ETemplet_DataUpdateTime',
			width: 135,isDate: true,hasTime: true,hidden:true,sortable: true,
			menuDisabled: true,defaultRenderer: true
		});
		return columns;
	},
	showMemoText:function(value, meta){
		var me=this	;
        var val=value.replace(/(^\s*)|(\s*$)/g, ""); 	
		val = val.replace(/\\r\\n/g, "<br />");
        val = val.replace(/\\n/g, "<br />");
		var v = "" + value;
		var index1=v.indexOf("</br>");
		if(index1>0)v=v.substring(0,index1);
		if(v.length > 0)v = (v.length > 38 ? v.substring(0, 38) : v);
		if(value.length>38){
			v= v+"...";
		}
        var qtipValue = "<p border=0 style='vertical-align:top;font-size:12px; word-break:break-all;'>" + value + "</p>";
        meta.tdAttr = 'data-qtip="' + qtipValue + '"';
        return v
	},
	/**下载*/
	downExcelTemplet: function(ETempletId) {
		var me = this;
		var ExcelTempletUrl = '/QMSReport.svc/QMS_UDTO_GetExcelTemplet';
		var url = JShell.System.Path.getRootUrl(ExcelTempletUrl);
		url += '?templetID=' + ETempletId + '&operateType=0';
		window.open(url);
	},
	onAddClick: function() {
		var me = this;
		me.fireEvent('onAddClick', me);
	},
	onEditClick: function() {

	},
	EditClick: function(ETempletId, SectionID) {
		var me = this;
		me.openForm(ETempletId, 'edit', SectionID,'2');
	},
	onSaveClick: function() {
		var me = this,
			records = me.getSelectionModel().getSelection();
		for(var i in records) {
			var ETemplet_Id = records[i].get("ETemplet_Id");
			var ETemplet_IsUse = records[i].get("ETemplet_IsUse");
			me.UpdateIsUse(ETemplet_Id, ETemplet_IsUse);
		}
		if(me.IsTrue == true) {
			me.onSearch();
		}
	},
	/**更新是否使用*/
	UpdateIsUse: function(id, IsUse) {
		var me = this;
		var url = me.editUrl;
		url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
		var entity = {
			IsUse: IsUse,
			Id: id
		};
		var fields = 'Id,IsUse';
		var params = {
			entity: entity,
			fields: fields
		};
		if(!params) return;
		params = Ext.JSON.encode(params);
		JShell.Server.post(url, params, function(data) {
			if(data.success) {
				if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.hideTimes);
				me.IsTrue = true;
			} else {
				var msg = data.msg;
				if(msg == JShell.Server.Status.ERROR_UNIQUE_KEY) {
					msg = '有重复';
				}
				JShell.Msg.error(msg);
			}
		}, false);
	},
	/**打开预览窗口*/
	openPdfForm: function(hasColse, hasSave, url) {
		var me = this;
		var maxWidth = document.body.clientWidth - 380;
		var height = document.body.clientHeight - 60;
		var config = {
			width: maxWidth,
			height: height,
			hasColse: hasColse,
			hasSave: hasSave,
			URL: url,
			listeners: {
				save: function(win) {
					me.Grid.onSearch();
					win.hide();
				},
				onSaveClick: function(win) {
					me.fireEvent('onSaveClick', win);
				}
			}
		};
		JShell.Win.open('Shell.class.qms.equip.templet.ereportdata.PreviewApp', config).show();
	},
	openForm: function(id, formtype, SectionID,NO) {
		var me = this;
		if(!NO){
			NO='1';
		}
		JShell.Win.open('Shell.class.qms.equip.templet.Form', {
			formtype: formtype,
			ETempletId: id,
			PK: id,
			height: 340,
			width: 720,
			SectionID: SectionID,
			SUB_WIN_NO: NO,
			 maximizable: false, //是否带最大化功能
			listeners: {
				save: function(p) {
					p.close();
					me.onSearch();
				}
			}
		}).show();
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			search = buttonsToolbar.getComponent('search').getValue(),
			params = [];

		var where = "",
			arr = [],
			url = JShell.System.Path.ROOT + me.selectUrl;

		if(me.BDictTreeId && me.BDictTreeId.toString().length > 0) {
			where = 'id=' + me.BDictTreeId + '^';
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
		//默认条件
		if(me.defaultWhere && me.defaultWhere != '') {
			arr.push(me.defaultWhere);
		}
		//内部条件
		if(me.internalWhere && me.internalWhere != '') {
			arr.push(me.internalWhere);
		}
		//外部条件
		if(me.externalWhere && me.externalWhere != '') {
			arr.push(me.externalWhere);
		}
		if(arr.length > 0) {
			where += '(' + arr.join(" and ") + ')';
		}
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',') + '&where=' + JcallShell.String.encode(where);

		return url;
	},
	/**预览仪器质量记录PDF文件URL*/
	getUrl: function(rec) {
		var me = this;
		var ETempletId = rec.get('ETemplet_Id');
		var url = JShell.System.Path.ROOT + '/QMSReport.svc/QMS_UDTO_PreviewExcelTemplet';
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'templetID=' + ETempletId + '&operateType=1';
		return url;
	},
	/**删除一条数据*/
	delOneById: function(index, id) {
		var me = this;
		var url = (me.delUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.delUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'id=' + id;

		setTimeout(function() {
			JShell.Server.get(url, function(data) {
				var record = me.store.findRecord(me.PKField, id);
				if(data.success) {
					if(record) {
						record.set(me.DelField, true);
						record.commit();
					}
					me.delCount++;
				} else {
					me.delErrorCount++;
					if(record) {
						record.set(me.DelField, false);
						record.set('ErrorInfo', data.msg);
						record.commit();
					}
				}
				if(me.delCount + me.delErrorCount == me.delLength) {
					me.hideMask(); //隐藏遮罩层
					if(me.delErrorCount == 0) {
						me.onSearch();
					} else {
						JShell.Msg.error('该对象被引用，不能删除！');
					}
				}
			});
		}, 100 * index);
	},
	/**删除模板及相关数据*/
	onSaveDelTempletData:function(){
		var me=this;
		var records = me.getSelectionModel().getSelection();
		if(records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		var id =records[0].get('ETemplet_Id');
		if(!id) return;
		Ext.MessageBox.show({
			title: '操作确认消息',
			msg: '确认删除模板及其相关质量记录数据吗？数据删除后不能恢复！',
			buttons: Ext.MessageBox.OKCANCEL,
			icon: Ext.Msg.QUESTION,
			fn: function(btn) {
				if(btn == 'ok') {
					var	url = me.delTempletDataUrl;
					url += '?id=' + id+'&isDelTempletData=true';
					url = JShell.System.Path.getRootUrl(url);
					JShell.Server.get(url, function(data) {
						if(data.success) {
							me.onSearch();
						} else {
							var msg = data.msg;
							JShell.Msg.error(msg);
						}
					});
				}
			},
			icon: Ext.MessageBox.QUESTION
		});
	},
	/**加载数据前*/
	onBeforeLoad: function() {
		var me = this;
		me.disableControl(); //禁用 所有的操作功能
		if (!me.defaultLoad) return false;
		me.getView().update();
		me.store.proxy.url = me.getLoadUrl(); //查询条件
		me.store.proxy.extraParams= {
			sort:Ext.encode(me.defaultOrderBy)
//			sort:JSON.stringify(me.defaultOrderBy)
        };
	},
     /**修改模板信息 */
    onShowEditForm: function (id,SectionID) {
        var me = this;
        JShell.Win.open('Shell.class.qms.equip.templet.EditForm', {
            maximizable: false, //是否带最大化功能
            title:'修改模板信息 ',
            formtype:'edit',
            ETempletId:id,
            SectionID: SectionID,
            PK:id,
            listeners:{
                save:function(p,id){
                    p.close();
                    me.onSearch();
                }
            }
        }).show();
    }
});