$(function () {
	//外部参数
	var params = JcallShell.getRequestParams(true);
	//获取退费信息服务地址
	var SEARCH_REFUND_INFO_URL = JcallShell.System.Path.ROOT + 
		"/ServerWCF/ZhiFangWeiXinAppService.svc/WXAS_BA_SearchRefundFormInfoByUOFCode";
		
	//退费信息列表数据
	var INFO_DATA_LIST = null;
	
	//获取订单数据
	function getInfoData(callback){
		var url = SEARCH_REFUND_INFO_URL + '?UOFCode=' + params.UOFCODE;
		//订单ID,状态,退费申请时间,退费原因,退费金额
		//退款处理人,退款处理开始时间,退款处理完成时间,退款处理说明
		//退款审批人,退款审批开始时间,退款审批完成时间,退款审批说明
		//退款发放人,退款发放开始时间,退款发放完成时间,退款发放说明
//		var FIELDS = [
//			'Id','Status','RefundApplyTime','RefundReason','RefundPrice',
//			'RefundOneReviewManName','RefundOneReviewStartTime','RefundOneReviewFinishTime','RefundOneReviewReason',
//			'RefundTwoReviewManName','RefundTwoReviewStartTime','RefundTwoReviewFinishTime','RefundTwoReviewReason',
//			'RefundThreeReviewManName','RefundThreeReviewStartTime','RefundThreeReviewFinishTime','RefundThreeReviewReason'
//		];
//		url += '?fields=OSManagerRefundForm_' + FIELDS.join(',OSManagerRefundForm_') + '&UOFCode=' + params.ID;
		
        $("#loading-div").modal({ backdrop: 'static', keyboard: false });
        JcallShell.Server.ajax({
        	showError:true,
            url: url
        }, function (data) {
            $("#loading-div").modal("hide");
            callback(data);
        });
	}
	//初始化数据
	function initData(callback){
		//获取订单数据
		getInfoData(function(data){
			if (data.success == true) {
				INFO_DATA_LIST = data.value || {};//可选数据
				callback();
			} else {
                showError(data.msg);
            }
		});
	}
	
	//显示错误信息
    function showError(msg) {
        $("#info").html('<div style="margin:20px 10px;color:#169ada;text-align:center;">' + msg + '</div>');
    }
    
    //初始化内容
	function initContent(){
		var list = INFO_DATA_LIST || [],
			len = list.length,
			html = [];
			
		for(var i=0;i<len;i++){
			html.push(getInfoHtml(list[i]));
		}
		
		$("#info").html(html.join(""));
	}
	
	//更改订单信息内容
	function getInfoHtml(rowData){
		var data = rowData || {},
			info = getInfoTemplet(),
			html = [];
		
		//申请：1
		//一审中,一审通过,一审退回：2,3,4
		//二审中,二审通过,二审退回：5,6,7
		//财务退回,退款完成,退款异常：8,10,11
		
		var Status = data.Status + "";
		var StatusName = "";
		switch (Status){
			case "4"://一审退回
				StatusName = "退款申请中";
				info = getTrackTable(data,info,1);
				break;
			case "1"://申请
			case "2"://一审中
			case "7"://二审退回
				StatusName = "退款处理中";
				info = getTrackTable(data,info,2);
				break;
			case "3"://一审通过
			case "5"://二审中
			case "8"://财务退回
				StatusName = "退款审批中";
				info = getTrackTable(data,info,3);
				break;
			case "6"://二审通过
				StatusName = "退款发放中";
				info = getTrackTable(data,info,4);
				break;
			case "10":
				StatusName = "退款完成";
				info = getTrackTable(data,info,5);
				break;
			case "11":
				StatusName = "退款异常";
				info = getTrackTable(data,info,5);
				break;
			default:
				break;
		}
		
		//价格,状态,退费原因
		var price = parseFloat(data.RefundPrice || "0").toFixed(2);
		info = info.replace(/{Price}/g, '￥' + price);
		info = info.replace(/{StatusName}/g, StatusName);
		info = info.replace(/{RefundReason}/g, data.RefundReason || "");
		html.push(info);
		
		return html.join("");
	}
	//获取流程列表
	function getTrackTable(rowData,info,type){
		var data = rowData || {},
			iConClass = 'table_td_icon_div_',
			textClass = 'table_td_text_div_',
			lineClass = 'table_td_line_v_div_',
			CreateChecked = 'uncheck',
			OneCheched = 'uncheck',
			TwoChecked = 'uncheck',
			ThreeChecked = 'uncheck',
			OverChecked = 'uncheck';
			
		//退款申请中
		if(type >= 1){
			CreateChecked = 'checked';
		}
		//退款处理中
		if(type >= 2){
			OneCheched = 'checked';
		}
		//退款审批中
		if(type >= 3){
			TwoChecked = 'checked';
		}
		//退款审批中
		if(type >= 4){
			ThreeChecked = 'checked';
		}
		//退款完成
		if(type >= 5){
			OverChecked = 'checked';
		}
		
//		var CreateTime = JcallShell.Date.toString(data.RefundApplyTime) || "";
//		var OneTime = JcallShell.Date.toString(data.RefundApplyTime) || "";
//		var TwoTime = JcallShell.Date.toString(data.RefundOneReviewFinishTime) || "";
//		var ThreeTime = JcallShell.Date.toString(data.RefundTwoReviewFinishTime) || "";
//		var OverTime = JcallShell.Date.toString(data.RefundThreeReviewFinishTime) || "";

		var CreateTime = JcallShell.Date.toString(data.RAT) || "";
		var OneTime = JcallShell.Date.toString(data.RAT) || "";
		var TwoTime = JcallShell.Date.toString(data.ROneReFT) || "";
		var ThreeTime = JcallShell.Date.toString(data.RTwoReFT) || "";
		var OverTime = JcallShell.Date.toString(data.RThreeReFT) || "";
		
		if(CreateTime){CreateTime = CreateTime.slice(5,16) + "";}
		if(OneTime){OneTime = OneTime.slice(5,16) + "";}
		if(TwoTime){TwoTime = TwoTime.slice(5,16) + "";}
		if(ThreeTime){ThreeTime = ThreeTime.slice(5,16) + "";}
		if(OverTime){OverTime = OverTime.slice(5,16) + "";}
		
		//退款申请
		info = info.replace(/{CreateIcon}/g, iConClass + CreateChecked);
		info = info.replace(/{CreateText}/g, textClass + CreateChecked);
		info = info.replace(/{CreateLine}/g, lineClass + CreateChecked);
		info = info.replace(/{CreateTime}/g, CreateTime);
		//退款处理
		info = info.replace(/{OneIcon}/g, iConClass + OneCheched);
		info = info.replace(/{OneText}/g, textClass + OneCheched);
		info = info.replace(/{OneLine}/g, lineClass + OneCheched);
		info = info.replace(/{OneTime}/g, OneTime);
		//退款审批
		info = info.replace(/{TwoIcon}/g, iConClass + TwoChecked);
		info = info.replace(/{TwoText}/g, textClass + TwoChecked);
		info = info.replace(/{TwoLine}/g, lineClass + TwoChecked);
		info = info.replace(/{TwoTime}/g, TwoTime);
		//退款审批
		info = info.replace(/{ThreeIcon}/g, iConClass + ThreeChecked);
		info = info.replace(/{ThreeText}/g, textClass + ThreeChecked);
		info = info.replace(/{ThreeLine}/g, lineClass + ThreeChecked);
		info = info.replace(/{ThreeTime}/g, ThreeTime);
		//退款完成
		info = info.replace(/{OverIcon}/g, iConClass + OverChecked);
		info = info.replace(/{OverText}/g, textClass + OverChecked);
		info = info.replace(/{OverLine}/g, lineClass + OverChecked);
		info = info.replace(/{OverTime}/g, OverTime);
		
		return info;
	}
	//获取订单模板
	function getInfoTemplet(){
		var templet = 
		'<div style="text-align:center;margin:30px 0 10px 0;font-size:12px;color:#6E6E6E;">' +
			'<div style="padding:0;font-size:24px;">{Price}</div>' +
			'<div style="padding:0;font-size:12px;">(退款金额)</div>' +
			'<div style="padding-top:10px;color:orange;font-size:12px;">{StatusName}</div>' +
		'</div>' +
		'<div>' +
			'<table style="width:100%;margin:10px 0;font-size:12px;color:#6E6E6E;">' +
				'<tr>' +
					'<td style="min-width:60px;vertical-align:text-top;padding-bottom:15px;">' +
						'<div>退款原因</div>' +
					'</td>' +
					'<td class="table_td_emp">&nbsp;</td>' +
					'<td style="padding-bottom:15px;" colspan="3">' +
						'<div style="text-align:right;">{RefundReason}</div>' +
					'</td>' +
				'</tr>' +
				'<tr>' +
					'<td rowspan="13" style="min-width:60px;vertical-align:text-top;padding-bottom:15px">' +
						'<div>退款进度</div>' +
					'</td>' +
					'<td class="table_td_emp">&nbsp;</td>' +
					'<td><div class="{CreateIcon}"></div></td>' +
					'<td><div class="{CreateText}">提出申请</div></td>' +
					'<td><div class="table_td_time_div">{CreateTime}</div></td>' +
				'</tr>' +
				'<tr>' +
					'<td class="table_td_emp">&nbsp;</td>' +
					'<td><div class="{CreateLine}"></div></td>' +
					'<td><div>&nbsp;</div></td>' +
					'<td><div>&nbsp;</div></td>' +
				'</tr>' +
				'<tr>' +
					'<td class="table_td_emp">&nbsp;</td>' +
					'<td><div class="{OneLine}"></div></td>' +
					'<td><div>&nbsp;</div></td>' +
					'<td><div>&nbsp;</div></td>' +
				'</tr>' +
				'<tr>' +
					'<td class="table_td_emp">&nbsp;</td>' +
					'<td><div class="{OneIcon}"></div></td>' +
					'<td><div class="{OneText}">退款处理中</div></td>' +
					'<td><div class="table_td_time_div">{OneTime}</div></td>' +
				'</tr>' +
				'<tr>' +
					'<td class="table_td_emp">&nbsp;</td>' +
					'<td><div class="{TwoLine}"></div></td>' +
					'<td><div>&nbsp;</div></td>' +
					'<td><div>&nbsp;</div></td>' +
				'</tr>' +
				'<tr>' +
					'<td class="table_td_emp">&nbsp;</td>' +
					'<td><div class="{TwoLine}"></div></td>' +
					'<td><div>&nbsp;</div></td>' +
					'<td><div>&nbsp;</div></td>' +
				'</tr>' +
				'<tr>' +
					'<td class="table_td_emp">&nbsp;</td>' +
					'<td><div class="{TwoIcon}"></div></td>' +
					'<td><div class="{TwoText}">退款审批中</div></td>' +
					'<td><div class="table_td_time_div">{TwoTime}</div></td>' +
				'</tr>' +
				'<tr>' +
					'<td class="table_td_emp">&nbsp;</td>' +
					'<td><div class="{TwoLine}"></div></td>' +
					'<td><div>&nbsp;</div></td>' +
					'<td><div>&nbsp;</div></td>' +
				'</tr>' +
				'<tr>' +
					'<td class="table_td_emp">&nbsp;</td>' +
					'<td><div class="{ThreeLine}"></div></td>' +
					'<td><div>&nbsp;</div></td>' +
					'<td><div>&nbsp;</div></td>' +
				'</tr>' +
				'<tr>' +
					'<td class="table_td_emp">&nbsp;</td>' +
					'<td><div class="{ThreeIcon}"></div></td>' +
					'<td><div class="{ThreeText}">退款发放中</div></td>' +
					'<td><div class="table_td_time_div">{ThreeTime}</div></td>' +
				'</tr>' +
				'<tr>' +
					'<td class="table_td_emp">&nbsp;</td>' +
					'<td><div class="{ThreeLine}"></div></td>' +
					'<td><div>&nbsp;</div></td>' +
					'<td><div>&nbsp;</div></td>' +
				'</tr>' +
				'<tr>' +
					'<td class="table_td_emp">&nbsp;</td>' +
					'<td><div class="{OverLine}"></div></td>' +
					'<td><div>&nbsp;</div></td>' +
					'<td><div>&nbsp;</div></td>' +
				'</tr>' +
				'<tr>' +
					'<td class="table_td_emp">&nbsp;</td>' +
					'<td><div class="{OverIcon}"></div></td>' +
					'<td><div class="{OverText}">退款完成</div></td>' +
					'<td><div class="table_td_time_div">{OverTime}</div></td>' +
				'</tr>' +
			'</table>'
		'</div>';
		return templet;
	}
	
	//初始化页面
	function initHtml(){
		initData(function(){
			//初始化内容
			initContent();
		});
	}
	
	//初始化页面
	initHtml();
});