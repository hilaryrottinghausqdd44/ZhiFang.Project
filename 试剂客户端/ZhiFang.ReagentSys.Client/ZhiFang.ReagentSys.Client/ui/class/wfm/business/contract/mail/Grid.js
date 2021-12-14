/**
 * 合同邮寄
 * @author liangyl	
 * @version 2017-03-17
 */
Ext.define('Shell.class.wfm.business.contract.mail.Grid', {
    extend: 'Shell.class.wfm.business.contract.basic.Grid',

    title: '合同邮寄列表',
	
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
			
			//签署人=自己 or 状态=评审通过
			var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
			var info1 = JShell.System.ClassDict.getClassInfoByName('PContractStatus','正式签署');
			var info2 = JShell.System.ClassDict.getClassInfoByName('PContractStatus','已邮寄');

//			me.defaultWhere = "(pcontract.ContractStatus='" + info1.Id + "')";


//			me.defaultWhere = "pcontract.SignManID=" + userId + 
//				" or pcontract.ContractStatus='" + info1.Id + "'";
			me.defaultWhere = "(pcontract.ContractStatus='" + info1.Id + "'" + " or pcontract.ContractStatus='" + info2.Id + "')";
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
			text: '邮寄',
			align: 'center',
			width: 40,
			style:'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					var SignManID = record.get('PContract_SignManID');
					var Status = record.get('PContract_ContractStatus');
					var info = JShell.System.ClassDict.getClassInfoById('PContractStatus',Status);
					var Name = info ? info.Name : '';
					
					//签署人=null and 状态=评审通过
					if(!SignManID && Name == '正式签署'){
						meta.tdAttr = 'data-qtip="<b>合同正式签署</b>"';
						return 'button-edit hand';
					}else{
						return 'button-actionedit hand';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get(me.PKField);
					var SignManID = rec.get('PContract_SignManID');
					var Status = rec.get('PContract_ContractStatus');
					var info = JShell.System.ClassDict.getClassInfoById('PContractStatus',Status);
					var Name = info ? info.Name : '';
					
					//签署人=null and 状态=评审通过
					if(!SignManID && Name == '正式签署'){
				     	me.openEditForm(id);
					}
				}
			}]
		}, {
            text: '付款单位ID', dataIndex: 'PContract_PayOrgID',  hidden: true, hideable: false
        }, {
            text: '付款单位', dataIndex: 'PContract_PayOrg',  hidden: true, hideable: false
        }, {
            text: '客户ID', dataIndex: 'PContract_PClientID',  hidden: true, hideable: false
        }, {
            text: '客户', dataIndex: 'PContract_PClientName',  hidden: true, hideable: false
        });
			
		return columns;
	},
    /**修改*/
    openEditForm: function (id) {
        var me = this;
        JShell.Win.open('Shell.class.wfm.business.contract.mail.EditPanel', {
        	SUB_WIN_NO:'1',//内部窗口编号
            //resizable:false,
            title:'合同邮寄',
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