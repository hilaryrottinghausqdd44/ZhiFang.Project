/**
 * 财务对账
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.balance.App',{
    extend:'Ext.tab.Panel',
    title:'财务对账',
    
    padding:1,
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
	},
	initComponent:function(){
		var me = this;
		
		me.items = me.createItems();
		
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems:function(){
		var me = this,
			items = [];
			
		items.push(Ext.create('Shell.class.pki.balance.LockAQuick',{
			title:'快捷锁定',
			itemId:'LockAQuick'
		}));
		items.push(Ext.create('Shell.class.pki.balance.LockAGrid',{
			title:'对账锁定',
			itemId:'lockAGrid'
		}));
//		items.push(Ext.create('Shell.class.pki.balance.LockACheck',{
//			title:'勾选锁定',
//			itemId:'LockACheck'
//		}));
//		items.push(Ext.create('Shell.class.pki.balance.NoContractPriceGrid',{
//			title:'无合同价项目',
//			itemId:'NoContractPriceGrid'
//		}));
		items.push(Ext.create('Shell.class.pki.balance.FreeSingleGrid',{
			title:'免单操作',
			itemId:'FreeSingleGrid'
		}));
		items.push(Ext.create('Shell.class.pki.balance.PersonGrid',{
			title:'个人开票',
			itemId:'PersonGrid'
		}));
		items.push(Ext.create('Shell.class.pki.balance.StepPriceGrid',{
			title:'阶梯价格计算',
			itemId:'StepPriceGrid'
		}));
		items.push(Ext.create('Shell.class.pki.balance.LockFGrid',{
			title:'财务锁定',
			itemId:'lockFGrid'
		}));
			
		return items;
	}
});