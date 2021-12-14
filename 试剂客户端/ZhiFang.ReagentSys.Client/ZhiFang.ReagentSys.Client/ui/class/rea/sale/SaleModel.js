/**
 * 供货单打印模板
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.sale.SaleModel', {
	/**模板1*/
	_Model_1:{
		/**类型名称*/
		Name:'A4模板',
		/**类型编号*/
		Type:'1',
		/**内容体*/
		Content:
			'LODOP.PRINT_INIT("试剂系统_供货单打印_供货单号{SaleDocNo}");'+
			'LODOP.SET_PRINT_PAGESIZE(2,0,0,"A4");'+
			
			'LODOP.ADD_PRINT_TEXT(10,0,"100%",30,"{Title}");'+
			'LODOP.SET_PRINT_STYLEA(0,"Alignment",2);'+
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",18);'+
			'LODOP.SET_PRINT_STYLEA(0,"FontName","华文行楷");'+
			'LODOP.SET_PRINT_STYLEA(0,"ItemType",1);'+
			
			'LODOP.ADD_PRINT_TEXT(50,50,200,20,"订货商：{LabName}");'+
			'LODOP.SET_PRINT_STYLEA(0,"ItemType",1);'+
			'LODOP.ADD_PRINT_TEXT(50,250,200,20,"供货日期：{SaleDate}");'+
			'LODOP.SET_PRINT_STYLEA(0,"ItemType",1);'+
			'LODOP.ADD_PRINT_TEXT(50,450,200,20,"供货单号：{SaleDocNo}");'+
			'LODOP.SET_PRINT_STYLEA(0,"ItemType",1);'+
			'LODOP.ADD_PRINT_TEXT(50,650,100,20,"第#页/共&页");'+
			'LODOP.SET_PRINT_STYLEA(0,"Alignment",3);'+
			'LODOP.SET_PRINT_STYLEA(0,"ItemType",2);'+
			'LODOP.SET_PRINT_STYLEA(0,"Horient",1);'+
			
			'LODOP.ADD_PRINT_TEXT(680,50,200,20,"合计数量：{Count}");'+
			'LODOP.SET_PRINT_STYLEA(0,"ItemType",1);'+
			'LODOP.ADD_PRINT_TEXT(680,250,200,20,"合计价格(小写)：{TotalPriceL}");'+
			'LODOP.SET_PRINT_STYLEA(0,"ItemType",1);'+
			'LODOP.ADD_PRINT_TEXT(680,450,400,20,"合计价格(大写)：{TotalPriceC}");'+
			'LODOP.SET_PRINT_STYLEA(0,"ItemType",1);'+
			
			'LODOP.ADD_PRINT_TEXT(705,50,200,20,"备注：{Memo}");'+
			'LODOP.SET_PRINT_STYLEA(0,"ItemType",1);'+
			
			'LODOP.ADD_PRINT_TEXT(730,50,200,20,"制表人(手签)：");'+
			'LODOP.SET_PRINT_STYLEA(0,"ItemType",1);'+
			'LODOP.ADD_PRINT_TEXT(730,250,400,20,"审核人(手签)：");'+
			'LODOP.SET_PRINT_STYLEA(0,"ItemType",1);'+
			'LODOP.ADD_PRINT_TEXT(730,450,200,20,"发货人(手签)：");'+
			'LODOP.SET_PRINT_STYLEA(0,"ItemType",1);'+
			'LODOP.ADD_PRINT_TEXT(730,700,200,20,"客户签收(手签)：");'+
			'LODOP.SET_PRINT_STYLEA(0,"ItemType",1);'+
			
			'LODOP.ADD_PRINT_TABLE(80,"2%","96%",400,"{TABLE}");'+
			'LODOP.SET_PRINT_STYLEA(0,"Vorient",3);',
			
		/**数据行内容模板*/
		rowForamt:
			'<tr>'+
				'<td><div align=center>{No}</div></td>'+
				'<td><div align=center>{ProdGoodsNo}</div></td>'+
				'<td><div align=center>{GoodsName}</div></td>'+
				'<td><div align=center>{UnitMemo}</div></td>'+
				'<td><div align=center>{GoodsQty}</div></td>'+
				'<td><div align=center>{GoodsUnit}</div></td>'+
				'<td><div align=center>{LotNo}</div></td>'+
				'<td><div align=center>{InvalidDate}</div></td>'+
				'<td><div align=center>{Price}</div></td>'+
				'<td><div align=center>{SumTotal}</div></td>'+
				'<td><div align=center>{ProdOrgName}</div></td>'+
				'<td><div align=center>{GoodsRegistNo}</div></td>'+
				'<td><div align=center>{GoodsStorageType}</div></td>'+
			'</tr>',
			
		/**获取打印内容*/
		getModelContent:function(data,type){
			var me = this,
				sale = me.Content;
				
			var tableHtml = me.getTbaleHtml();
				
			sale = sale
				.replace(/\{Title\}/g,'甘肃泰达商贸有限公司')
				.replace(/\{LabName\}/g,'青海红十字医院')
				.replace(/\{SaleDate\}/g,'2016-01-02 09:14:13')
				.replace(/\{SaleDocNo\}/g,'11141605110001')
				.replace(/\{Count\}/g,'100')
				.replace(/\{TotalPriceL\}/g,'29875000')
				.replace(/\{TotalPriceC\}/g,JShell.Number.getMoney('29875000'))
				.replace(/\{Memo\}/g,'这是一个备注信息')
				.replace(/\{TABLE\}/g,tableHtml);
				
			return sale;
		},
		/**获取列表内容*/
		getTbaleHtml:function(list){
			var me = this,
				html = [];
			
			html.push(
				'<table border=1 cellspacing=0 cellpadding=1 width=100% fontsize=8px>'+
					'<thead>'+
						'<tr>'+
							'<td><div align=center><b>序号</b></div></td>'+
						    '<td><div align=center><b>产品编码</b></div></td>'+
						    '<td><div align=center><b>产品名称</b></div></td>'+
						    '<td><div align=center><b>产品规格</b></div></td>'+
						    '<td><div align=center><b>数量</b></div></td>'+
						    '<td><div align=center><b>单位</b></div></td>'+
						    '<td><div align=center><b>批号</b></div></td>'+
						    '<td><div align=center><b>有效期</b></div></td>'+
						    '<td><div align=center><b>单价</b></div></td>'+
						    '<td><div align=center><b>总价</b></div></td>'+
						    '<td><div align=center><b>生成厂家</b></div></td>'+
						    '<td><div align=center><b>注册证号</b></div></td>'+
						    '<td><div align=center><b>储存条件</b></div></td>'+
						'</tr>'+
					'</thead>'+
					'<tbody>'
			);
			
			for(var i=0;i<100;i++){
				var row = me.rowForamt
					.replace(/\{No\}/g,i+1)
					.replace(/\{ProdGoodsNo\}/g,i+1)
					.replace(/\{GoodsName\}/g,'乙型肝炎病毒e抗体测定试剂盒')
					.replace(/\{UnitMemo\}/g,'4*100T')
					.replace(/\{GoodsQty\}/g,'10')
					.replace(/\{GoodsUnit\}/g,'盒')
					.replace(/\{LotNo\}/g,'QHRCH123')
					.replace(/\{InvalidDate\}/g,'2016-11-01')
					.replace(/\{Price\}/g,'29875')
					.replace(/\{SumTotal\}/g,'298750')
					.replace(/\{ProdOrgName\}/g,'雅培贸易(上海)有限公司')
					.replace(/\{GoodsRegistNo\}/g,'国食药监械(进)字2013第3400287号')
					.replace(/\{GoodsStorageType\}/g,'2-8℃');
				html.push(row);
			}
			
			html.push('</tbody>');
			
			html.push('</table>');
			
			return html.join("");
		}
	},
	
	/**获取打印内容*/
	getModelContent:function(type,data){
		var me = this,
			barcode = "";
			
		if(me['_Model_' + type]){
			barcode = me['_Model_' + type].getModelContent(data);
		}
		
		return barcode;
	},
	/**获取模板列表*/
	getModelList:function(){
		var me = this,
			list = [];
		
		list.push([me._Model_1.Type,me._Model_1.Name]);//模型1
		
		return list;
	}
});