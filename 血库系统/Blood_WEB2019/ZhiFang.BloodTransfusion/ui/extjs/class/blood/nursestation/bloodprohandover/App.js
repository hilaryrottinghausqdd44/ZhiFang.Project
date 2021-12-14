/**
 * 护士站--血袋接收
 * @author longfc
 * @version 2020-03-17
 */
Ext.define('Shell.class.blood.nursestation.bloodprohandover.App', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '血袋接收',
	/**内容周围距离*/
	bodyPadding: '2px',
	/**His调用传入的就诊号*/
	ADMID: "",
	/**接收His调用传入的参数*/
	defaultParams: {},
	
	//接收传入参数
	getParams: function() {
		var me = this;
		var params = JShell.Page.getParams();
		//His病区Id
		if (params["wardId"]) me.defaultParams.WardId = params["wardId"];
		//his科室Id
		if (params["deptId"]) me.defaultParams.DeptId = params["deptId"];
		//his医生Id/his护士Id
		if (params["doctorId"]) me.defaultParams.DoctorId = params["doctorId"];
		//His的就诊号
		if (params["admId"]) me.defaultParams.AdmId = params["admId"];	
			me.ADMID = me.defaultParams.AdmId;
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.onListenersPanel();
	},
	initComponent: function() {
		var me = this;
		me.getParams();
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		var clientWidth = document.body.clientWidth;
		var width1 = 330;
		if (clientWidth >= 1366) {
			width1 = 380;
		}
		var width2 = clientWidth - width1;
		//发血主单列表
		me.OutDocGrid= Ext.create('Shell.class.blood.nursestation.bloodprohandover.out.DocGrid', {
			region: 'west',
			width: width1,
			header: true,
			itemId: 'OutDocGrid',
			/**His调用传入的就诊号*/
			AdmId: me.ADMID,
			split: true,
			collapsible: false
		});
		//右侧区域信息
		me.ShowPanel = Ext.create("Shell.class.blood.nursestation.bloodprohandover.ShowPanel", {
			region: 'center',
			header: true,
			width: width2,
			itemId: 'ShowPanel'
		});
		return [me.OutDocGrid, me.ShowPanel];
	},
	/*列表事件监听**/
	onListenersPanel: function() {
		var me = this;
		me.OutDocGrid.on({
			itemclick: function(grid, record, item, index, e, eOpts) {
				JShell.Action.delay(function() {
					me.loadData(record);
				}, null, 300);
			},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					me.loadData(record);
				}, null, 300);
			},
			nodata: function(p) {
				me.onNodata();
			},
			onAddTrans: function(p) {
				me.addBloodBagRetrieve();
			}
		});
	},
	loadData:function(record){
		var me = this;
		var outFormId = record.get("BloodBOutForm_Id");
		me.ShowPanel.loadData(outFormId);
	},
	onNodata:function(){
		var me = this;
		me.ShowPanel.onNodata();
	},
	/**
	 * 血袋回收登记	
	 */
	addBloodBagRetrieve: function() {
		var me = this;
		var maxWidth = document.body.clientWidth;
		maxWidth = document.body.clientWidth * 0.98;
		var height1 = document.body.clientHeight;
		height1 = document.body.clientHeight * 0.95;
		var src = JShell.System.Path.ROOT + "/ui/layui/views/bloodtransfusion/nursestation/bloodprohandover/index.html";
		if(me.defaultParams){
			var arr = [];
			arr.push("wardId=" + me.defaultParams.WardId); //病区编码
			arr.push("deptId=" + me.defaultParams.DeptId); //科室编码
			arr.push("doctorId=" + me.defaultParams.DoctorId); //医生编码
			arr.push("admId=" + me.defaultParams.AdmId); //就诊号
			var params = encodeURI(arr.join("&")); //IE需要进行编码
			src += ((src.indexOf('?') == -1 ? '?' : '&') + "t=" + new Date().getTime() + "&" + params);
			//console.log(src);
		}
		var html = '<iframe frameborder=0 width="100%" height="100%" allowtransparency="true" scrolling=auto src="' + src +
			'"></iframe>';
		JShell.Win.open('Shell.ux.panel.AppPanel', {
			title: "血袋接收",
			layout: 'fit', //设置布局模式为fit，能让frame自适应窗体大小
			resizable: true,
			width: maxWidth,
			height: height1,
			modal: true,
			border: 0,
			frame: false,
			html: html,
			listeners: {
				close: function() {
					me.OutDocGrid.onSearch();
				}
			}
		}).show();
	}
});
