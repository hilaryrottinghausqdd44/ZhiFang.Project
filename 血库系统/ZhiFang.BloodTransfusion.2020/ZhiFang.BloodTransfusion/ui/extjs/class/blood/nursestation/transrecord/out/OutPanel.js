/**
 * 输血过程记录:发血记录主单列表
 * @author longfc
 * @version 2020-02-21
 */
Ext.define('Shell.class.blood.nursestation.transrecord.out.OutPanel', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '发血记录信息',
	/**开启加载数据遮罩层*/
	hasLoadMask: true,
	/**His调用传入的就诊号*/
	AdmId: "",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.onListeners();
	},
	initComponent: function() {
		var me = this;
		//me.addEvents('dtlselect', 'nodata', 'save');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.DocGrid = Ext.create("Shell.class.blood.nursestation.transrecord.out.DocGrid", {
			region: 'north',
			header: false,
			height: 160,
			border: false,
			itemId: 'DocGrid',
			/**His调用传入的就诊号*/
			AdmId: me.AdmId
		});
		me.DtlGrid = Ext.create('Shell.class.blood.nursestation.transrecord.out.DtlGrid', {
			region: 'center',
			header: false,
			itemId: 'DtlGrid',
			split: false,
			border: false,
			collapsible: false
		});
		return [me.DocGrid, me.DtlGrid];
	},
	/*程序列表的事件监听**/
	onListeners: function() {
		var me = this;
		me.DocGrid.on({
			itemclick: function(grid, record, item, index, e, eOpts) {
				me.loadDtl(record);
			},
			select: function(RowModel, record) {
				me.loadDtl(record);
			},
			nodata: function(p) {
				me.clearData();
			},
			onAddTrans: function(p) {
				me.addTrans();
			}
		});
		me.DtlGrid.on({
			itemclick: function(grid, record, item, index, e, eOpts) {
				JShell.Action.delay(function() {
					me.fireEvent('dtlselect', me, record);
				}, null, 500);
			},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					me.fireEvent('dtlselect', me, record);
				}, null, 500);
			},
			nodata: function(p) {
				me.clearData();
			},
			onAddTrans: function(p) {
				me.addTrans();
			}
		});
	},
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		if (me.hasLoadMask) {
			me.body.mask(text);
		} //显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if (me.hasLoadMask) {
			me.body.unmask();
		} //隐藏遮罩层
	},
	/**外部调用*/
	onSearch: function() {
		var me = this;
		me.DocGrid.onSearch();
	},
	clearData: function() {
		var me = this;
		//me.DtlGrid.PK = null;
		me.DtlGrid.store.removeAll();
		me.fireEvent('nodata', me);
	},
	loadDtl: function(record) {
		var me = this;
		JShell.Action.delay(function() {
			var id = record.get(me.DocGrid.PKField);
			me.DtlGrid.PK = id;
			me.DtlGrid.onSearch();
		}, null, 300);
	},
	//获取输血登记是否需要交接登记完成后
	getISNeedBloodBag: function() {
		var me = this;
		var paraValue = "";
		if (JcallShell && JcallShell.BLTF) {
			paraValue = "" + JcallShell.BLTF.RunParams.Lists.BloodTransISNeedBloodBag.Value;
			if (!paraValue) paraValue = ""+JShell.BLTF.cachedata.getCache("BloodTransISNeedBloodBag");
		}
		return paraValue;
	},
	addTrans: function() {
		var me = this;
		var records = me.DtlGrid.getSelectionModel().getSelection();
		//自定义复选框列
		/* me.DtlGrid.store.each(function(record) {
			var check = record.get("BloodBOutItem_CheckColumn");
			if (check) {
				records.push(record);
			}
		}); */

		if (records.length <= 0) {
			JShell.Msg.error("请选择血袋后再进行登记!");
			return;
		}
		//判断是否需要系统运行参数"输血登记是否需要交接登记完成后"
		var paraValue = me.getISNeedBloodBag();
		//console.log(paraValue);
		var alertInfo="";
		if(paraValue=="1"){
			for (var i = 0; i < records.length; i++) {
				var handoverCompletion = ""+records[i].get("BloodBOutItem_HandoverCompletion");
				if (handoverCompletion!="3") {//未交接完成
					var cname = ""+records[i].get("BloodBOutItem_BloodABO_CName");
					alertInfo="血袋为:"+cname+",未进行血袋交接登记,不能进行输血登记!";
					break;
				}
			}
		}
		if(alertInfo){
			JShell.Msg.error(alertInfo);
			return;
		}
		
		//判断是批量新增还是批量修改或单个修改
		var formtype = "add";
		var outDtlIdStr = ""; //批量登记的发血明细Id
		var transId = null; //单个修改的输血过程记录主单Id
		var isBatchEdit = false; //是否批量修改
		if (records.length > 1) {
			var info = "";
			var outDtlArr = [];
			for (var i = 0; i < records.length; i++) {
				var outId = records[i].get("BloodBOutItem_Id");;
				outDtlArr.push(outId);
				var transId1 = records[i].get("BloodBOutItem_BloodTransForm_Id");
				if (transId1 && transId1.length > 0) {
					isBatchEdit = true;
					formtype = "edit";
					//break;
				}
			}
			if (info.length > 0) {
				JShell.Msg.error(info);
				return;
			}
			outDtlIdStr = outDtlArr.join(",");
		} else if (records.length == 1) {
			outDtlIdStr = records[0].get("BloodBOutItem_Id");
			transId = records[0].get("BloodBOutItem_BloodTransForm_Id");
			if (transId) formtype = "edit";
		}
		//批量新增或单个修改
		var app = "Shell.class.blood.nursestation.transrecord.register.App";
		if (isBatchEdit == true) {
			transId = "";
			app = "Shell.class.blood.nursestation.transrecord.editbatch.App";
		}
		var maxWidth = document.body.clientWidth - 10; //* 0.98;
		var height1 = document.body.clientHeight * 0.96;
		if (height1 > 768) {
			//height1 = 768;
		}
		JShell.Win.open(app, {
			resizable: true,
			width: maxWidth,
			height: height1,
			formtype: formtype,
			outDtlIdStr: outDtlIdStr, //批量修改时使用
			outDtlRrecords: records,
			PK: transId, //单个修改时使用
			listeners: {
				save: function(p, id) {
					p.close();
					me.onSearch();
					//me.fireEvent('save', me);
				}
			}
		}).show();
	}
});
