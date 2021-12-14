var Shell = Shell || {};
Shell.confirm = Shell.confirm || {};
Shell.confirm.common = Shell.confirm.common || {};
//合并行的条件字段
Shell.confirm.common.SCANCODEFILE = "MixSerial";

Shell.confirm.common.getBarCodeOfMixSerial = function(model, isgetpid) {
	if(!model) return "";

	var mixSerial = Shell.confirm.common.getMixSerialByBarCodeMgr(model, isgetpid);
	if(!mixSerial) return "";
	//条码类型
	var barCodeMgr = "" + model.BarCodeMgr;
	switch(barCodeMgr) {
		case "1": //盒条码试剂的混合条码处理
			//处理(5)(6)
			if(mixSerial.indexOf("=") > -1 && mixSerial.split("=").length == 2) {
				mixSerial.split("=")[1];
				var tempArr = mixSerial.split("|");
				if(tempArr.length >= 3) {
					mixSerial = "" + tempArr[2];
				}
			} else {
				//兼容旧的条码规则
				var tempArr = mixSerial.split("|");
				if(!tempArr) return mixSerial;
				if(tempArr.length == 3) {
					mixSerial = "" + tempArr[2];
				} else if(tempArr.length == 4) {
					mixSerial = "" + tempArr[3];
				} else if(tempArr.length >= 9) {
					mixSerial = +tempArr[7] + "|" + tempArr[8] + "|" + tempArr[9];
				}
			}
			break;
		default:
			break;
	}
	return mixSerial;
};
/***
 * 获取比较同一种试剂的比较判断依据值
 * 优先采用PSaleDtlID,其次为MixSerial(兼容旧的数据的PSaleDtlID为空时),最后为ProdGoodsNo+LotNo或当前明细主键值
 * @param {Object} model
 */
Shell.confirm.common.getMixSerialByBarCodeMgr = function(model, isgetpid) {
	if(!model) return "";
	var mixSerial = "";
	if(isgetpid && isgetpid == true) {
		//上级ID(指拆分后原明细的主键ID值)
		var psaleDtlID = model.PSaleDtlID;
		if(psaleDtlID && (psaleDtlID != "0" || psaleDtlID != 0)) mixSerial = psaleDtlID;
	}
	if(mixSerial) return mixSerial;

	if(!mixSerial) mixSerial = model[Shell.confirm.common.SCANCODEFILE];

	if(mixSerial) return mixSerial;
	//盒条码试剂的混合条码处理
	var barCodeMgr = "" + model.BarCodeMgr;
	switch(barCodeMgr) {
		case "1":
			//如果混合条码还为空,厂商产品编号+货品批号
			if(!mixSerial) {
				var ProdGoodsNo = model.ProdGoodsNo;
				var LotNo = model.LotNo;
				mixSerial = ProdGoodsNo + '+' + LotNo;
			}
			break;
		default:
			break;
	}
	//mixSerial还为空时取当前明细ID
	if(!mixSerial) mixSerial = model["Id"];

	return mixSerial;
};
Shell.confirm.common.getTitlePre = function(model) {
	var titlePre = "";
	//条码类型
	var barCodeMgr = "" + model.BarCodeMgr;
	switch(barCodeMgr) {
		case "1": //盒条码	
			titlePre = '<span style="padding:2px;color:red; border:solid 1px red">盒</span>&nbsp;&nbsp;';
			break;
		case "0": //批条码	
			titlePre = '<span style="padding:2px;color:red; border:solid 1px red">批</span>&nbsp;&nbsp;';
			break;
		default:
			break;
	}
	return titlePre;
};