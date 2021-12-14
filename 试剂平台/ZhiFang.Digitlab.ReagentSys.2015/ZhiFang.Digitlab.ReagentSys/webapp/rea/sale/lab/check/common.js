var Shell = Shell || {};
Shell.check = Shell.check || {};
Shell.check.common = Shell.check.common || {};
//合并行的条件字段
Shell.check.common.SCANCODEFILE = "BmsCenSaleDtl_MixSerial";

/***
 *  获取混合条码分割后的值,混合条码格式有
 * (1)上级ID(指拆分后原明细的主键ID值)
 * (2)平台的盒装混全条码格式:ZFRP|1|1116|078-K138-01|T11131|2017-05-30|110001118|5459663006151236684|1|111
 * (3)平台的非盒装混合条码格式:5459663006151236684,(验收待定)
 * (4)第三方平台的混合条码格式:11131|1747070530|20170530|111,分割后取前三段
 * (5)第三方平台接口:https://m.roche-diag.cn/barcode?num=07404880340190(3.0)170413120355000546|20984201|20180930,分割后取后两段
 *  (6)第三方平台接口:https://m.roche-diag.cn/barcode?num=07403045838122(4.0)170711210905000302|20990002|20180531||0304583,分割后取后两段
 * @param {Object} model
 * @param {Object} isgetpid是否取上级ID值为最优先级,当扫码操作时不取
 */
Shell.check.common.getSplitMixSerial = function(model, isgetpid) {
	if(!model) return "";
	var mixSerial = Shell.check.common.getMixSerialByBarCodeMgr(model, isgetpid);
	if(!mixSerial) return "";
	//条码类型
	var barCodeMgr = model.BmsCenSaleDtl_BarCodeMgr;
	switch(barCodeMgr) {
		case "1":
			//盒条码试剂的混合条码处理
			//处理(5)
			if(mixSerial.indexOf("=") > -1 && mixSerial.split("=").length == 2) {
				mixSerial = mixSerial.split("=")[1];
				var tempArr = mixSerial.split("|");
				if(!tempArr) return mixSerial;
				if(tempArr.length >= 3) {
					mixSerial = "" + tempArr[1] + "|" + tempArr[2];
				}
			} else {
				//兼容旧的条码规则
				var tempArr = mixSerial.split("|");
				if(!tempArr) return mixSerial;
				if(tempArr.length == 3) {
					mixSerial = "" + tempArr[1] + "|" + tempArr[2];
				} else if(tempArr.length == 4) {
					mixSerial = tempArr[3];
				} else if(tempArr.length > 4) {
					mixSerial = tempArr[7];
				} else {
					mixSerial = tempArr[0];
				}
			}
			break;
		default:
			break;
	}
	return mixSerial;
};
Shell.check.common.getBarCodeOfMixSerial = function(model, isgetpid) {
	if(!model) return "";

	var mixSerial = Shell.check.common.getMixSerialByBarCodeMgr(model, isgetpid);
	if(!mixSerial) return "";
	//条码类型
	var barCodeMgr = model.BmsCenSaleDtl_BarCodeMgr;
	switch(barCodeMgr) {
		case "1": //盒条码试剂的混合条码处理
			//处理(4)
			if(mixSerial.indexOf("=") > -1 && mixSerial.split("=").length == 2) mixSerial.split("=")[1];

			var tempArr = mixSerial.split("|");
			if(!tempArr) return mixSerial;
			if(tempArr.length == 3) {
				mixSerial = "" + tempArr[2];
			} else if(tempArr.length == 4) {
				mixSerial = "" + tempArr[3];
			} else if(tempArr.length >= 9) {
				mixSerial = +tempArr[7] + "|" + tempArr[8] + "|" + tempArr[9];
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
Shell.check.common.getMixSerialByBarCodeMgr = function(model, isgetpid) {
	if(!model) return "";
	var mixSerial = "";
	if(isgetpid && isgetpid == true) {
		//上级ID(指拆分后原明细的主键ID值)
		var psaleDtlID = model.BmsCenSaleDtl_PSaleDtlID;
		if(psaleDtlID && (psaleDtlID != "0" || psaleDtlID != 0)) mixSerial = psaleDtlID;
	}
	if(mixSerial) return mixSerial;

	if(!mixSerial) mixSerial = model[Shell.check.common.SCANCODEFILE];

	if(mixSerial) return mixSerial;
	//盒条码试剂的混合条码处理
	var barCodeMgr = model.BmsCenSaleDtl_BarCodeMgr;
	switch(barCodeMgr) {
		case "1":
			//如果混合条码还为空,厂商产品编号+货品批号
			if(!mixSerial) {
				var ProdGoodsNo = model.BmsCenSaleDtl_ProdGoodsNo;
				var LotNo = model.BmsCenSaleDtl_LotNo;
				mixSerial = ProdGoodsNo + '+' + LotNo;
			}
			break;
		default:
			break;
	}
	//mixSerial还为空时取当前明细ID
	if(!mixSerial) mixSerial = model["BmsCenSaleDtl_Id"];

	return mixSerial;
};
Shell.check.common.getTitlePre = function(model) {
	var titlePre = "";
	//条码类型
	var barCodeMgr = model.BmsCenSaleDtl_BarCodeMgr;
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