/**
 * @description 环境监测送检样本登记--基本父类
 * @author longfc
 * @version 2020-11-09
 */
Ext.define('Shell.class.assist.infection.basic.App',{
    extend:'Shell.ux.panel.AppPanel',
	
    title:'环境监测送检样本登记',
    /**开启加载数据遮罩层*/
    hasLoadMask: true,
	
	/**监测类型集合信息*/
	RecordTypeItemList: [],
	
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
	},
	initComponent:function(){
		var me = this;
		/* me.getRecordTypeItemList(function() {
			me.items = me.createItems();
		}); */
		me.callParent(arguments);
	},
	
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		if (me.hasLoadMask) {
			me.body.mask(text);
		} //显示遮罩层
		//me.disableControl(); //禁用所有的操作功能
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if (me.hasLoadMask) {
			me.body.unmask();
		} //隐藏遮罩层
		//me.enableControl(); //启用所有的操作功能
	},
	/**
	 * 事件联动处理
	 */
	onListenersLink:function(){
		var me=this;
		
		me.Grid.on({
			itemclick:function(v, record) {
				JShell.Action.delay(function(){
					me.loadData(record);
				},null,500);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function(){
					me.loadData(record);
				},null,500);
			},
			addclick:function(){
				me.Form.isAdd();
			},
			editclick:function(p,record){
				me.loadData(record);
			},
			nodata:function(){
				me.Form.SCRecordTypeId=null;
				me.Form.PK=null;
				me.Form.clearData();
				//me.Form.getForm().reset();
			}
		});
		me.Form.on({
			onBatchSubmit:function(p){
				me.onBatchSubmit(p);
			},
			onBarcodePrint:function(printType,modelType){
				me.onBarcodePrint(printType,modelType);
			},
			onObsolete:function(p){
				me.onBatchObsolete(p);
			},
			save:function(p,id){
				me.Grid.onSearch();
			}
		});
	},
	/**
	 * @description 获取监测类型集合信息
	 * @param {Object} callback
	 */
	getRecordTypeItemList: function(callback) {
		var me = this;
		//从缓存读取
		var list = JcallShell.BLTF.cachedata.getCache("RecordTypeItemList");
		if (list && list.length > 0) {
			me.RecordTypeItemList = list;
			return callback();
		}
		if (me.RecordTypeItemList.length > 0) {
			if (callback) {
				return callback();
			} else {
				return;
			}
		}
		var where =
			"screcordtype.ContentTypeID=10000 and screcordtype.IsUse=1";
		var sort = [{
			"property": "SCRecordType_DispOrder",
			"direction": "ASC"
		}];
		var url = JShell.System.Path.ROOT +
			'/ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchSCRecordTypeByHQL?isPlanish=true';
	
		url = url + "&where=" + JShell.String.encode(where);
		url = url + "&sort=" + JShell.JSON.encode(sort);
		JShell.Server.get(url, function(data) {
			if (data.success && data.value && data.value.list) {
				me.RecordTypeItemList = data.value.list;
			} else {
				me.RecordTypeItemList = [];
			}
			JcallShell.BLTF.cachedata.setCache("RecordTypeItemList", me.RecordTypeItemList)
			if (callback) callback();
		}, false);
	},
	/**
	 * @description 加载表单信息
	 * @param {Object} record
	 */
	loadData:function(record){
		var me=this;
		var id=record.get(me.Grid.PKField);
		var statusID=""+record.get("GKSampleRequestForm_StatusID");
		var recordTypeId=""+record.get("GKSampleRequestForm_SCRecordType_Id");
		
		me.Form.SCRecordTypeId=recordTypeId;
		if(statusID=="0"){
			me.Form.isEdit(id);
		}else{
			me.Form.isShow(id);
		}
	},
	/**
	 * @description 条码打印
	 * @param {Object} printType
	 * @param {Object} modelType
	 */
	onBarcodePrint:function(printType,modelType){
		var me=this;
		
		var records = me.Grid.getSelectionModel().getSelection(),
			len = records.length;
		if (len == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		
		var LODOP = me.Form.getCLodop();
		//打印机选择
		var printer = me.Form.getPrinter();
		
		//Lodop打印内容字符串数组
		var LodopStr = [];
		
		var title = me.Form.BarCodeModel.getModelTitle(modelType);
		//模板标题
		LodopStr.push(title);
		LodopStr.push(me.getBarcodeByRecords(modelType));
		eval(LodopStr.join(""));
		
		if (printer)
			LODOP.SET_PRINTER_INDEXA(printer.getValue());
		
		var result = null;
		if (printType == 1) { //直接打印
			result = LODOP.PRINT();
			//更新条码打印次数
			if (result != 0) {
				//me.onUpdatePrintCount();
			}
		} else if (printType == 2) { //预览打印
			result = LODOP.PREVIEW();
		} else if (printType == 3) { //维护打印
			result = LODOP.PRINT_SETUP();
		} else if (printType == 4) { //设计打印
			result = LODOP.PRINT_DESIGN();
		}
	},
	/**根据选中的数据产生条码*/
	getBarcodeByRecords: function(modelType) {
		var me = this,
			records = me.Grid.getSelectionModel().getSelection(),
			len = records.length,
			list = [];
	
		for (var i = 0; i < len; i++) {
			list.push(records[i].data);
		}
		return me.getBarcodeContentByRecords(list,modelType);
	},
	/**根据数据产生条码*/
	getBarcodeContentByRecords: function(list,modelType) {
		var me = this;
		var len = list.length,
			content = [];
		var printOne = true;
		for (var i = 0; i < len; i++) {
			var rec = list[i];
	
			var barCode = rec.GKSampleRequestForm_BarCode;
			//条码不存在的不打印
			if (!barCode) continue;
	
			//打印的数量
			var num = 1;
			if (num <= 0) continue;
	
			for (var j = 0; j < num; j++) {
				var monitorType2=""+rec.GKSampleRequestForm_MonitorType;
				if(monitorType2=="2"){
					monitorType2="(感控监测)";
				}else{
					monitorType2="(科室监测)";
				}
				//一维码模板
				var barcode = me.Form.BarCodeModel.getModelContent(modelType, {
					DataAddTime: JShell.Date.toString(rec.GKSampleRequestForm_DataAddTime, true),
					ReqDocNo: rec.GKSampleRequestForm_ReqDocNo,
					MonitorType:monitorType2 ,
					BarCode: rec.GKSampleRequestForm_BarCode,
					DeptCName: rec.GKSampleRequestForm_DeptCName, //
	
					GoodsName: rec.GKSampleRequestForm_SCRecordType_CName, //监测类型
					ItemResult1: rec.GKSampleRequestForm_ItemResult1, //样品信息1
					Sampler: rec.GKSampleRequestForm_Sampler,
					SampleDate: JShell.Date.toString(rec.GKSampleRequestForm_SampleDate, true)
				});
				content.push(barcode);
			}
		}
		return content.join("");
	},
	/**
	 * @description 批量确认暂存申请单
	 * @param {Object} record
	 */
	onBatchSubmit:function(p){
		var me=this;
		var result=me.Grid.verifyBatchSubmit();
		if(result==false) return;
		
		JShell.Msg.confirm({
			title: "确定要批量确认当前选择的申请信息吗?"
		}, function(but) {
			if (but != "ok") return ;
			
			me.showMask("批量确认处理中,请稍等!");
			me.Grid.onBatchSubmit(function(){
				me.hideMask(); //隐藏遮罩层
			});
		});
	},
	/**
	 * @description 作废处理
	 */
	onBatchObsolete: function() {
		var me = this;
		var result=me.Grid.verifyBatchObsolete();
		if(result==false) return;
		
		JShell.Msg.confirm({
			title: "确定要作废当前选择的申请信息吗?"
		}, function(but) {
			if (but != "ok") return ;
			
			me.showMask("申请作废处理中,请稍等!");
			me.Grid.onBatchObsolete(function(){
				me.hideMask(); //隐藏遮罩层
			});
		});
	}
});
	