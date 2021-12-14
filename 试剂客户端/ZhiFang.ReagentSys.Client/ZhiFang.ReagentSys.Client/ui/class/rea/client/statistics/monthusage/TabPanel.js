/**
 * 使用出库月用量统计
 * @author longfc
 * @version 2018-10-24
 */
Ext.define('Shell.class.rea.client.statistics.monthusage.TabPanel', {
	extend: 'Ext.tab.Panel',

	title: '月用量统计',

	/**月结单Id*/
	PK: null,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			/**页签切换事件处理*/
			tabchange: function(tabPanel, newCard, oldCard, eOpts) {
				var me = this;
				switch(newCard.itemId) {
					case 'DtlGrid':
						if(me.PK != me.DtlGrid.PK || me.DtlGrid.defaultLoad == false)
							me.DtlGrid.onSearch();
						break;
					case 'PdfPanel':
						if(me.PK != me.PdfPanel.PK || me.PdfPanel.defaultLoad == false)
							me.PdfPanel.loadData(me.PK);
						break;
					default:
						break
				}
			}
		});
		me.PdfPanel.on({
			onLaunchFullScreen: function() {
				me.fireEvent('onLaunchFullScreen', me);
			},
			onExitFullScreen: function() {
				me.fireEvent('onExitFullScreen', me);
			}
		});
		me.DtlGrid.on({
			onLaunchFullScreen: function() {
				me.fireEvent('onLaunchFullScreen', me);
			},
			onExitFullScreen: function() {
				me.fireEvent('onExitFullScreen', me);
			}
		});
		me.activeTab = 0;
	},
	initComponent: function() {
		var me = this;
		me.addEvents('onLaunchFullScreen', 'onExitFullScreen');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.DtlGrid = Ext.create('Shell.class.rea.client.statistics.monthusage.DtlGrid', {
			title: '出库使用量统计',
			header: true,
			border: false,
			itemId: 'DtlGrid'
		});
		me.PdfPanel = Ext.create('Shell.class.rea.client.statistics.monthusage.PdfPanel', {
			title: 'PDF报表',
			header: true,
			hidden: true,
			border: false,
			itemId: 'PdfPanel'
		});
		var appInfos = [me.DtlGrid, me.PdfPanel];
		return appInfos;
	},
	loadData: function(record) {
		var me = this;
		if(record)
			me.isShow(record);
		else
			me.clearData();
	},
	isShow: function(record) {
		var me = this;
		var id = record.get("ReaMonthUsageStatisticsDoc_Id");
		me.PK = id;
		me.DtlGrid.PK = id;
		me.DtlGrid.internalWhere = "reamonthusagestatisticsdtl.DocID=" + id;
		me.DtlGrid.onSearch();

		me.PdfPanel.clearData();
		me.PdfPanel.PK = id;
	},
	clearData: function() {
		var me = this;
		me.nodata();
	},
	nodata: function() {
		var me = this;
		me.PK = null;
		
		
		me.DtlGrid.PK = null;
		me.DtlGrid.internalWhere = "";
		me.DtlGrid.clearData();
		
		me.PdfPanel.PK = null;
		me.PdfPanel.clearData();
	}
});