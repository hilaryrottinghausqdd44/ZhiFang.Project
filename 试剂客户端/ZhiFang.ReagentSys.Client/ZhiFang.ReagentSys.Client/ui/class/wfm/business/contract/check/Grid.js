/**
 * 合同验收列表
 * @author Jcall
 * @version 2016-11-14
 */
Ext.define('Shell.class.wfm.business.contract.check.Grid', {
    extend: 'Shell.class.wfm.business.contract.basic.Grid',

    title: '合同验收列表',
	
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
			var StatusID = me.getComponent('buttonsToolbar').getComponent('StatusID');
			var List=JShell.System.ClassDict.PContractStatus;
			
			if(StatusID.store.data.items.length==0){
			     StatusID.loadData(me.getStatusData(List));
			     StatusID.setValue(me.defaultStatusValue);
			}
			//客户属于自己
			me.getClientIds(function(ids){
				var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;  
				ids = ids.length > 0 ? ids : ['-1'];
				me.defaultWhere = 'pcontract.PClientID in(' + ids.join(',') + ')';
				/* 签署人或者销售负责人和申请人是自己的都可以看  @author liangyl @version 2017-07-27 */
				me.defaultWhere+=' or (pcontract.SignManID='+userId+' or pcontract.PrincipalID='+userId+' or pcontract.ApplyManID='+userId+')';
				me.load(null, true, autoSelect);
			});
			
    	});
	},
    /**创建数据列*/
	createGridColumns:function(){
		var me = this,
			columns = me.callParent(arguments);
			
		columns.splice(3,0,{
			xtype: 'actioncolumn',
			text: '验收',
			align: 'center',
			width: 40,
			style:'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || '-1';
					var ApplyManID = record.get('PContract_ApplyManID') + '';
					var Status = record.get('PContract_ContractStatus');
					var info = JShell.System.ClassDict.getClassInfoById('PContractStatus',Status);
					var Name = info ? info.Name : '';
					
					//申请人=自己 and 状态=正式签署
					if(ApplyManID == userId && Name == '正式签署'){
						meta.tdAttr = 'data-qtip="<b>合同验收</b>"';
						return 'button-edit hand';
					}else{
						return 'button-actionedit hand';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get(me.PKField);
					var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || '-1';
					var ApplyManID = rec.get('PContract_ApplyManID') + '';
					var Status = rec.get('PContract_ContractStatus');
					var info = JShell.System.ClassDict.getClassInfoById('PContractStatus',Status);
					var Name = info ? info.Name : '';
					//申请人=自己 and 状态=正式签署
					if(ApplyManID == userId && Name == '正式签署'){
					    me.openEditForm(id);
					}
				}
			}]
		});
				
		columns.push({
			text:'申请人ID',dataIndex:'PContract_ApplyManID',hidden:true,hideable:false
		});
		
		return columns;
	},
    /**修改*/
    openEditForm: function (id) {
        var me = this;
        JShell.Win.open('Shell.class.wfm.business.contract.check.EditPanel', {
        	SUB_WIN_NO:'1',//内部窗口编号
            //resizable:false,
            title:'合同验收',
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