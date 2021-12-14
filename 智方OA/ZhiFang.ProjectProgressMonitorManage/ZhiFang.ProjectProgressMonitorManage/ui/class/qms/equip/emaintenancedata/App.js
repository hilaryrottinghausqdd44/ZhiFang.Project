/**
 * 质量数据登记
 * @author liangyl
 * @version 2018-10-24
 */
Ext.define('Shell.class.qms.equip.emaintenancedata.App', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '质量数据登记',
	width: 1000,
	height: 800,
	//模板Id
	TempletID: '',
	//模板项目代码
	TempletTypeCode: '',
	selectUrl: '/QMSReport.svc/QMS_UDTO_PreviewPdf',
	//从外边传参时间控件是否只读,默认是true，不可改, false（可改） 
    ISEDITDATE:false,
    //质量记录登记页面保存数据后是否直接预览
    IsSaveDataPreview:'0',
    hideTimes:2000,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.SimpleGrid.on({
			itemclick: function(v, record) {
				me.onSelect(record);
			},
			select: function(RowModel, record) {
				me.onSelect(record);
			},
			nodata:function(p){
				me.ShowPanel.clearData();
			}
		});
		me.ShowPanel.on({
			save : function(){//保存成功后回调
				var records = me.SimpleGrid.getSelectionModel().getSelection();
				if (records.length == 0) {
					JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
					return;
				}
				me.ShowPanel.saveCallBack(me.SimpleGrid,records[0]);
			},
			del : function(tbgrid){//tb列表删除后回调
				var records = me.SimpleGrid.getSelectionModel().getSelection();
				if (records.length == 0) {
					JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
					return;
				}
				me.ShowPanel.getTempletState(me.SimpleGrid,records[0]);
			},
			nodata:function(){//RecordGrid 数据为空时回调
				var records = me.SimpleGrid.getSelectionModel().getSelection();
				if (records.length == 0) {
					JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
					return;
				}
				me.ShowPanel.gridNoData(me.SimpleGrid,records[0]);
			}
		});
	},

	initComponent: function() {
		var me = this;
		
		me.title = me.title || "模板日常维护";
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/***/
	createItems: function() {
		var me = this;
		var TempletPanel='Shell.class.qms.equip.register.SimpleGrid';
		me.SimpleGrid = Ext.create(TempletPanel, {
			border: true,
			title: '模板列表',
			region: 'west',
			width: 420,
			header: false,
		    //从外边传参时间控件是否只读,默认是true，不可改, false（可改） 
            ISEDITDATE:me.ISEDITDATE,
			split: true,
			collapsible: true,
			collapseMode:'mini',
			name: 'SimpleGrid',
			itemId: 'SimpleGrid'
		});
		me.ShowPanel = Ext.create('Shell.class.qms.equip.emaintenancedata.ShowPanel', {
			border: true,
			title: '操作列表',
			region: 'center',
			header: false,
			ISEDITDATE:me.ISEDITDATE,
			itemId: 'ShowPanel'
		});
	   
		return [me.SimpleGrid, me.ShowPanel];
	},
	onSelect :function(record){
		var me = this;
		JShell.Action.delay(function() {
			var id = record.get("ETemplet_Id");
			var ShowFillItem =  record.get("ETemplet_ShowFillItem");
			me.ShowPanel.simpleGridRec=record;
			me.ShowPanel.TempletBatNo='';
			//为空 不显示列表
			if(!ShowFillItem){
				me.ShowPanel.isShowGrid(false);
				me.ShowPanel.changeShowDailyBtn();
				me.ShowPanel.onSelect(record,'');
			}else{
				me.ShowPanel.isShowGrid(true);
				//载入按钮不能显示
				me.ShowPanel.isShowDailyBtn('0');
			    me.ShowPanel.searchData(id,record);
			}
		}, null, 200);
	}
});