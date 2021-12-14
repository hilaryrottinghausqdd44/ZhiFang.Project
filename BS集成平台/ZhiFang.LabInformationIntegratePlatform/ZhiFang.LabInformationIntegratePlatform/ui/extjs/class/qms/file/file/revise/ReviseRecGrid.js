/**
 * 历史修订记录
 * @author liangyl
 * @version 2019-01-15
 */
Ext.define('Shell.class.qms.file.file.revise.ReviseRecGrid', {
	extend: 'Shell.class.qms.file.file.show.Grid',
	title: '历史修订记录',
	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/CommonService.svc/QMS_UDTO_SearchReviseFFileListByFileID?isPlanish=true',
    FFileId:null,
    PK:null,
    /**默认排序字段*/
	defaultOrderBy:[],
	cellTip:false,
    afterRender: function() {
		var me = this;
		me.callParent(arguments);
	    Ext.override(Ext.ToolTip, {
			maxWidth: 780
		});
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		buttonsToolbar.hide();
		if(me.PK)me.onSearch();
		me.on({
			itemdblclick: function(grid, record, item, index, e, eOpts) {
				me.openShowTabPanel(record);
			},
			onShowClick: function() {
				var me = this;
				var records = me.getSelectionModel().getSelection();
				if(records && records.length < 1) {
					JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
					return;
				}
				me.openShowTabPanel(records[0]);
			},
			itemmouseenter : function (view,record,  item,  index,e,  eOpts ){
                var gridColums = view.getGridColumns();
                var column = gridColums[e.getTarget(view.cellSelector).cellIndex];  
				view.el.dom.MyData='';
				if(column.dataIndex){
					view.el.dom.MyData=record.get(column.dataIndex);
				}
			}
		});
		me.showTip();
	},
	
	
	/**创建数据列*/
	createNewColumns: function() {
		var me = this;
		var columns = [];
		columns.push({
			text: '文档ID',dataIndex: 'FFile_Id',
			isKey: true,hidden: true,hideable: false,defaultRenderer: true
		}, {
			text: '版本号',dataIndex: 'FFile_VersionNo',width: 100,
			sortable: false,menuDisabled: true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return value;
            }
		},{
			text: '修订号',dataIndex: 'FFile_ReviseNo',width: 100,
			sortable: false,menuDisabled: true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return value;
            }
		}, {
			text: '修订内容',dataIndex: 'FFile_Memo',hidden:false,
			width: 100,sortable: false,menuDisabled: true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				var v=me.showQtipValue(meta, record,value);
				return v;
            }
		},{
			text: '文档标题',dataIndex: 'FFile_Title',width: 150,
			sortable: false,menuDisabled: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return value;
            }
		}, {
			xtype: 'actioncolumn',text: '附件',align: 'center',
			width: 40,style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					return 'button-show hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get('FFile_Id');
					//me.showAttachment(id);
					me.onSetAttachment(rec);
				}
			}],
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return value;
            }
		}, {
			text: '修订时间',dataIndex: 'FFile_ReviseTime',
			width: 130,
			hasTime: true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				var v = JShell.Date.toString(value, false) || '';
				return v;
            }
		},{
			text: '修订人',dataIndex: 'FFile_Revisor_CName',
			width: 80,hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return value;
            }
		},{
			text: '审核人',dataIndex: 'FFile_CheckerName',
			width: 80,sortable: false,
			menuDisabled: true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return value;
            }
		}, {
			text: '审批人',dataIndex: 'FFile_ApprovalName',
			width: 80,sortable: false,
			menuDisabled: true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return value;
            }
		},{
			text: '发布人',dataIndex: 'FFile_PublisherName',
			width: 100,sortable: false,
			menuDisabled: true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return value;
            }
		}, {
			text: '发布时间',dataIndex: 'FFile_PublisherDateTime',
			width: 130,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				var v = JShell.Date.toString(value, false) || '';
				return v;
            }
		},{
			text: '开始日期',dataIndex: 'FFile_BeginTime',width: 130,
//			isDate: true,hasTime: true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				var v = JShell.Date.toString(value, true) || '';
				return v;
            }
		},{
			text: '结束日期',dataIndex: 'FFile_EndTime',width: 130,
//			isDate: true,hasTime: true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				var v = JShell.Date.toString(value, true) || '';
				return v;
            }
		},{
			text: '是否允许评论',dataIndex: 'FFile_IsDiscuss',
			hidden: true,hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return value;
            }
		});
		return columns;
	},

	showQtipValue: function(meta, record,value) {
		var me = this;
        var val=value.replace(/(^\s*)|(\s*$)/g, ""); 
        val = val.replace(/\\r\\n/g, "");
        val = val.replace(/\\n/g, "");
        val = val.replace(/\<p><img/g, "<br /><img");
        val = val.replace(/\<p>/g, "");
        val = val.replace(/\<\/p>/g, "");
        val = val.replace(/\<br\/>/g, "");
		var v = val;
		if(v.length > 0)v = (v.length > 15 ? v.substring(0, 15) : v);
		if(value.length>15){
			v= v+"...";
		}
		if(value.length<15 && value.length>0){
			v= v+"...";
		}
		return v;
	},
    /**overwrite查询条件*/
	createdefaultWhere: function() {
		var me = this;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			arr = [];
        me.defaultWhere="";
		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;

		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');
        url +='&fileId='+me.PK;
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
	showTip:function(){
		var view = this.getView();
        this.tip = new Ext.ToolTip({
            target : view.el,
            delegate : '.x-grid-cell-inner',
            trackMouse : true,
            renderTo : document.body,
            itemId:'myTip',
            html:'',
            layout:'fit',
		    autoHide: true,
		    closable: false,
		    
			showDelay:100,
			hideDelay:200,
			dismissDelay :0,

            listeners : {
                beforeshow : function updateTipBody(tip,e) {
                    if (Ext.isEmpty(tip.triggerElement.innerText) || !tip.target.dom.MyData) {
                        return false;
                    }
                    tip.body.dom.innerHTML = tip.target.dom.MyData;
                }
            }
        });
	}
});