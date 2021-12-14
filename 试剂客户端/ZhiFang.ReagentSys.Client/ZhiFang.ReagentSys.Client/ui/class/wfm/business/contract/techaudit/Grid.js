/**
 * 合同技术评审列表
 * @author Jcall
 * @version 2016-11-14
 */
Ext.define('Shell.class.wfm.business.contract.techaudit.Grid', {
    extend: 'Shell.class.wfm.business.contract.basic.Grid',

    title: '合同技术评审列表',
	
    /**是否启用刷新按钮*/
    hasRefresh: true,
    /**是否启用查询框*/
    hasSearch: true,
	
    /**查询数据*/
	onSearch: function(autoSelect) {
		var me = this;
		
		JShell.System.ClassDict.init('ZhiFang.Entity.ProjectProgressMonitorManage','PContractStatus',function(){
			if(!JShell.System.ClassDict.PContractStatus){
    			JShell.Msg.error('未获取到合同状态，请刷新列表');
    			return;
    		}
			
			//技术评审人=自己 or 状态 in（申请，技术评审）
			var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
			var info1 = JShell.System.ClassDict.getClassInfoByName('PContractStatus','申请');
			var info2 = JShell.System.ClassDict.getClassInfoByName('PContractStatus','商务已评');
			
			me.defaultWhere = "pcontract.TechReviewManID=" + userId + 
				" or pcontract.ContractStatus in('" + info1.Id + "','" + info2.Id + "')";
			var StatusID = me.getComponent('buttonsToolbar').getComponent('StatusID');
			var List=JShell.System.ClassDict.PContractStatus;
			
			if(StatusID.store.data.items.length==0){
			     StatusID.loadData(me.getStatusData(List));
			     StatusID.setValue(me.defaultStatusValue);
			}	
			me.load(null, true, autoSelect);
    	});
	},
    /**创建数据列*/
	createGridColumns:function(){
		var me = this,
			columns = me.callParent(arguments);
			
		columns.splice(3,0,{
			xtype: 'actioncolumn',
			text: '评审',
			align: 'center',
			width: 40,
			style:'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					var TechReviewManID = record.get('PContract_TechReviewManID');
					var Status = record.get('PContract_ContractStatus');
					var info = JShell.System.ClassDict.getClassInfoById('PContractStatus',Status);
					var Name = info ? info.Name : '';
					
					//技术评审人=null and 状态 in（申请，商务评审）
					if(!TechReviewManID && (Name == '申请' || Name == '商务已评')){
						meta.tdAttr = 'data-qtip="<b>合同技术评审</b>"';
						return 'button-edit hand';
					}else{
						return 'button-actionedit hand';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get(me.PKField);
					var TechReviewManID = rec.get('PContract_TechReviewManID');
					var Status = rec.get('PContract_ContractStatus');
					var info = JShell.System.ClassDict.getClassInfoById('PContractStatus',Status);
					var Name = info ? info.Name : '';
					//技术评审人=null and 状态 in（申请，商务评审）
					if(!TechReviewManID && (Name == '申请' || Name == '商务已评')){
					    me.openEditForm(id);
					}
				}
			}]
		});
			
		return columns;
	},
    /**修改*/
    openEditForm: function (id) {
        var me = this;
        JShell.Win.open('Shell.class.wfm.business.contract.techaudit.EditPanel', {
        	SUB_WIN_NO:'1',//内部窗口编号
            //resizable:false,
            title:'合同技术评审',
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