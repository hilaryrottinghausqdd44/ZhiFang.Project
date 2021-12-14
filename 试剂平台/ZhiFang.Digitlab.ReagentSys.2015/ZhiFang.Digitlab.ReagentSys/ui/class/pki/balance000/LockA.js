/**
 * 对账一次锁定
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.balance.LockA',{
    extend:'Ext.tab.Panel',
    title:'对账一次锁定',
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
	},
	initComponent:function(){
		var me = this;
		
		me.items = me.createItems();
		//创建挂靠功能栏
		me.dockedItems = [{
			dock:'top',
			xtype:'toolbar',
			items:[{
				xtype:'label',
            	html:'<div style="color:white;margin:5px;font-weight:bold;">' +
            		'对账锁定后，前处理不能更改该数据。对账锁定后会自动计算合同价格，' +
            		'可能需要一些时间，请耐心等待。</div>'
			}]
		}];
		
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
			title:'选择锁定',
			itemId:'LockAGrid'
		}));
			
		return items;
	}
});