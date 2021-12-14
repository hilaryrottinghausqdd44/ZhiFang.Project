/**
 * 仪器模板列表
 * @author liangyl
 * @version 2016-08-12
 */
Ext.define('Shell.class.qms.equip.templet.admin.Grid', {
	extend: 'Shell.class.qms.equip.templet.Grid',
	title: '仪器模板列表',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.BoolComboBox'
	],
	selectUrl: '/QMSReport.svc/QMS_UDTO_GetETempletByHRDeptID?isPlanish=true',
	/**删除仪器Excel模板及相关数据*/
	delTempletDataUrl:'/QMSReport.svc/QMS_UDTO_DelETemplet',
	IsTrue: false,
	SectionID: '',
	/**默认加载数据*/
	defaultLoad: false,
	BDictTreeId: '',
	hasDelTempletData:true,
	/**默认排序字段*/
	defaultOrderBy: [{property: 'ETemplet_DispOrder',direction: 'ASC'}],
	/**后台排序*/
	remoteSort: false,
	/**默认每页数量*/
	defaultPageSize: 200,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems: function() {
		var me = this,
		buttonToolbarItems = me.buttonToolbarItems || [];
		
    	buttonToolbarItems.push('refresh','-','add',{
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
		},'del','-',{
			text:'删除模板及相关数据',tooltip:'删除模板及相关数据',
			iconCls:'button-del',
			handler: function() {
				me.onSaveDelTempletData();
			}
		},{
			text:'删除模板数据',tooltip:'删除模板数据',
			iconCls:'button-del',
			handler: function() {
			    var records = me.getSelectionModel().getSelection();
				if(records.length != 1) {
					JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
					return;
				}
				var id =records[0].get('ETemplet_Id');
				if(!id) return;
				me.openDelmodelForm(id);
			}
		});
	   //查询框信息
		me.searchInfo = {width: 145,emptyText: '模板名称',isLike: true,
			itemId: 'search',fields: ['etemplet.CName']
		};
		buttonToolbarItems.push('->',{
			type: 'search',
			info: me.searchInfo
		});
		
		return buttonToolbarItems;
	},
	 /**指定日期删除模板数据*/
    openDelmodelForm: function (id) {
        var me = this;
        JShell.Win.open('Shell.class.qms.equip.templet.admin.Form', {
            maximizable: false, //是否带最大化功能
            title:'按指定日期删除模板数据',
            formtype:'add',
            ETempletId:id,
            listeners:{
                save:function(p,id){
                    p.close();
                    me.onSearch();
                }
            }
        }).show();
    }
	
});