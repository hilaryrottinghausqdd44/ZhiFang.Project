/**
 * 供货单打印模板
 * 打印供货单主单信息+明细列表
 * @author Jcall
 * @version 2017-06-10
 */
Ext.define('Shell.class.rea.sale.print.Model', {
	_getDocInfoUrl:'/ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDocById?isPlanish=true',
	_getDtlListUrl:'/ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDtlByHQL?isPlanish=true',
	
	//获取主单信息
	_getDocInfo:function(id,callback){
		var me = this,
			url = JShell.System.Path.ROOT + me._getDocInfoUrl;
			
		var fields = [
			"LabName","AccepterTime","CompanyName","SaleDocNo","Memo","SendOutTime",
			"LabAddress","LabContact","LabTel"
		];
		
		url += "&id=" + id + "&fields=BmsCenSaleDoc_" + fields.join(",BmsCenSaleDoc_");
			
		JShell.Server.get(url,function(data){
			if(data.success){
				callback(data.value);
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	},
	//获取明细列表信息
	_getDtlList:function(id,callback){
		var me = this,
			url = JShell.System.Path.ROOT + me._getDtlListUrl;
			
		var fields = ["Goods_CName","GoodsUnit","GoodsQty","LotNo","InvalidDate","ProdOrgName","RegisterNo","Comp_CName"];
		url += "&fields=BmsCenSaleDtl_" + fields.join(",BmsCenSaleDtl_");
		
		url += "&where=bmscensaledtl.BmsCenSaleDoc.Id=" + id
			
		JShell.Server.get(url,function(data){
			if(data.success){
				callback((data.value || {}).list);
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	},
	//合并数据
	_mergerData:function(list){
		var me = this,
			len = list.length,
			map = {},
			data = [];
			
		for(var i=0;i<len;i++){
			var ProdGoodsNo = list[i].BmsCenSaleDtl_ProdGoodsNo;
			var LotNo = list[i].BmsCenSaleDtl_LotNo;
			var GoodsSerial = ProdGoodsNo + '+' + LotNo;
			if(!map[GoodsSerial]){
				map[GoodsSerial] = list[i];
			}else{
				var GoodsQty = list[i].BmsCenSaleDtl_GoodsQty;
				map[GoodsSerial].BmsCenSaleDtl_GoodsQty = parseInt(GoodsQty) +
					parseInt(map[GoodsSerial].BmsCenSaleDtl_GoodsQty);
			}
		}
		var i=0;
		for(var m in map){
			data[i++] = map[m];
		}
		
		return data;
	},
	
	//获取内容HTML
	_getContentHtml:function(DocInfo,DtlList){
		var me = this,
			infoHtml = me._getDocInfoTemplet();
		
		//获取主单HTML
		infoHtml = infoHtml.replace(/\{实验室名称}/g,DocInfo.BmsCenSaleDoc_LabName);
		infoHtml = infoHtml.replace(/\{客户名称}/g,DocInfo.BmsCenSaleDoc_LabName);
		infoHtml = infoHtml.replace(/\{收货地址}/g,DocInfo.BmsCenSaleDoc_LabAddress);
		infoHtml = infoHtml.replace(/\{发货单号}/g,DocInfo.BmsCenSaleDoc_SaleDocNo);
		infoHtml = infoHtml.replace(/\{备注}/g,DocInfo.BmsCenSaleDoc_Memo);
		infoHtml = infoHtml.replace(/\{发货日期}/g,DocInfo.BmsCenSaleDoc_SendOutTime);
		infoHtml = infoHtml.replace(/\{联系人}/g,DocInfo.BmsCenSaleDoc_LabContact);
		infoHtml = infoHtml.replace(/\{联系电话}/g,DocInfo.BmsCenSaleDoc_LabTel);
		//获取明细列表HTML
		var len = DtlList.length,
			listHtml = [];
		for(var i=0;i<len;i++){
			var data = DtlList[i];
			var row = me._getDtlListTemplet();
			row = row.replace(/\{验收日期}/g,JShell.Date.toString(DocInfo.BmsCenSaleDoc_AccepterTime,true) || "");
			row = row.replace(/\{产品名称}/g,data.BmsCenSaleDtl_Goods_CName || "");
			row = row.replace(/\{规格型号}/g,data.BmsCenSaleDtl_GoodsUnit || "");
			row = row.replace(/\{数量}/g,data.BmsCenSaleDtl_GoodsQty || "");
			row = row.replace(/\{批号}/g,data.BmsCenSaleDtl_LotNo || "");
			row = row.replace(/\{效期}/g,JShell.Date.toString(data.BmsCenSaleDtl_InvalidDate,true) || "");
			row = row.replace(/\{外观质量包装}/g,"");
			row = row.replace(/\{厂商名称}/g,data.BmsCenSaleDtl_ProdOrgName || "");
			row = row.replace(/\{注册证号}/g,data.BmsCenSaleDtl_RegisterNo || "");
			row = row.replace(/\{供货商名称}/g,DocInfo.BmsCenSaleDoc_CompanyName || "");
			row = row.replace(/\{供货单联系人}/g,"");
			row = row.replace(/\{验收人签字}/g,"");
			
			listHtml.push(row);
		}

		infoHtml = infoHtml.replace(/\{列表}/g,listHtml.join(""));
		
		return infoHtml;
	},
	//获取主单模板
	_getDocInfoTemplet:function(){
		var temp = 
		'<style>table,th{border:none;height:60px} td{border: 1px solid #000;height:18px}</style>' +
		'<table border=0 cellSpacing=0 cellPadding=0  width="100%" bordercolor="#000000" style="border-collapse:collapse">' +
			'<caption>' + 
				'<div style="padding:1opx;"><b><font face="黑体" size="4">供货单</font></b></div>' +
				'<div style="text-align:left;">' +
					'<div style="float:left;padding:5px;width:100%;">客户名称：{客户名称}</div>' +
				'</div>' +
				'<div style="text-align:left;">' +
					'<div style="float:left;padding:5px;width:50%;">收货地址：{收货地址}</div>' +
					'<div style="float:left;padding:5px;width:20%;">发货单号：{发货单号}</div>' +
					'<div style="float:left;padding:5px;width:20%;x">联系人：{联系人}</div>' +
				'</div>' +
				'<div style="text-align:left;">' +
					'<div style="float:left;padding:5px;width:50%;">备注：{备注}</div>' +
					'<div style="float:left;padding:5px;width:20%;">发货日期：{发货日期}</div>' +
					'<div style="float:left;padding:5px;width:20%;x">联系电话：{联系电话}</div>' +
				'</div>' +
			'</caption>' +
			'<thead style="text-align:center;font-weight:bold;">' +
				'<td>验收日期</td>' +
				'<td>产品名称</td>' +
				'<td>规格型号</td>' +
				'<td>数量</td>' +
				'<td>批号</td>' +
				'<td>效期</td>' +
				'<td>外观质量包装</td>' +
				'<td>厂商名称</td>' +
				'<td>注册证号</td>' +
				'<td>供货商名称</td>' +
				'<td>供货单联系人</td>' +
				'<td>验收人签字</td>' +
			'</thead>' +
			'<tbody>' +
				'{列表}' +
			'</tbody>' +
		'</table>';
		
		return temp;
	},
	//获取明细列表行模板
	_getDtlListTemplet:function(){
		var temp = 
		'<tr>' +
			'<td>{验收日期}</td>' +
			'<td>{产品名称}</td>' +
			'<td>{规格型号}</td>' +
			'<td>{数量}</td>' +
			'<td>{批号}</td>' +
			'<td>{效期}</td>' +
			'<td>{外观质量包装}</td>' +
			'<td>{厂商名称}</td>' +
			'<td>{注册证号}</td>' +
			'<td>{供货商名称}</td>' +
			'<td>{供货单联系人}</td>' +
			'<td>{验收人签字}</td>' +
		'</tr>';
		
		return temp;
	},
	
	/**
	 * @public
	 * 获取打印内容
	 * @param {Object} id 供货单逐渐ID
	 * @param {Object} callback 回调函数
	 */
	getModelContent:function(id,callback){
		var me = this,
			DocInfo = {},
			DtlList = [];
		
		me._getDocInfo(id,function(info){
			DocInfo = info || {};
			me._getDtlList(id,function(list){
				DtlList = me._mergerData(list);
				var html = me._getContentHtml(DocInfo,DtlList);
				callback(html);
			});
		});
	}
});