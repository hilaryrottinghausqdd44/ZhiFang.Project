/**
 * 入账单信息
 * @author liangyl
 * @version 2017-06-02
 */
Ext.define('Shell.class.rea.recorded.basic.AddPanel', {
	extend: 'Ext.panel.Panel',
	title: '入帐',
	layout: 'border',
	bodyPadding: 1,
	width: 700,
	htight: 400,
	
	/**机构Id*/
	CENORG_ID: null,
	/**机构名称*/
	CENORG_NAME: null,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		var CheckGrid = me.getComponent('CheckGrid');
		var AccountPanel = me.getComponent('AccountPanel');
		//已选择的供货单
		var Grid = AccountPanel.getComponent('Grid');
		//供货单选择
		CheckGrid.on({
			itemdblclick: function(grid, record, item, index, e, eOpts) {
				//已选供货单添加一行
				Grid.addDateRec(record);
				//待选供货单删除一行
				CheckGrid.DelDataRec(record);
			},
			accept: function(grid, record) { //确定添加供货单和删除供货单
				//已选供货单添加多行
				Grid.addDateRecs(record);
				//待选供货单删除多行
				CheckGrid.DelDataRecS(record);
			}
		});
		Grid.on({
			onRemoveClick: function(records, grid) { //删除已选供货单行
				Grid.DelDataRecS(records, CheckGrid);
				if(records.length > 0) {
					CheckGrid.onSearch();
				}
			}
		});
		AccountPanel.on({
			save: function(panel) {
				me.fireEvent('save', me);
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this,
			items = [];
		items.push(Ext.create('Shell.class.rea.recorded.basic.censaledoc.CheckGrid', {
			region: 'center',
			itemId: 'CheckGrid',
			CENORG_ID: me.CENORG_ID
		}));
		items.push(Ext.create('Shell.class.rea.recorded.basic.AccountPanel', {
			region: 'west',//west
			width: 540,
			split: true,
			collapsible: true,
			itemId: 'AccountPanel',
			/**机构Id*/
			CENORG_ID: me.CENORG_ID,
			/**机构名称*/
			CENORG_NAME: me.CENORG_NAME,
			header: false
		}));

		return items;
	}
});