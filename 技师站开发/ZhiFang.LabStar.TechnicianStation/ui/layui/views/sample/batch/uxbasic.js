/**
	@name：批量处理公共方法
	@author：liangyl
	@version 2021-04-30
 */
layui.extend({
	uxutil: 'ux/util',
	uxbase: 'ux/base'
}).define(['uxutil','uxbase','table','laydate'], function(exports){
	"use strict";
	
	var $ = layui.$,
	    table =  layui.table,
		laydate = layui.laydate,
		uxbase = layui.uxbase,
		uxutil = layui.uxutil;
	
	 //根据指定样本号批量生成新样本号
	var batchCreateSampleNoUrl = uxutil.path.ROOT +'/ServerWCF/LabStarService.svc/LS_UDTO_BatchCreateSampleNoByCurSampleNo';
    	//过滤条件复选框属性列表
	var STATUS_LIST = [
		{ text: '验中', iconText: '检', color: '#FFB800', where: 'listestform.MainStatusID=0', checked: true },
		{ text: '验确认', iconText: '检', color: '#5FB878', where: 'listestform.MainStatusID=2', checked: true },
		{ text: '核', iconText: '审', color: '#009688', where: 'listestform.MainStatusID=3', checked: true },
		{ text: '审', iconText: '反', color: '#1E9FFF', where: '(listestform.MainStatusID=0 and listestform.CheckTime<>null)' },
		{ text: '', iconText: '废', color: '#c2c2c2', where: 'listestform.MainStatusID=-2' },
		{ text: '检', iconText: '复', color: '#2F4056', where: 'listestform.RedoStatus=1' }
	];
	//状态样色
	var ColorMap = {
		"危急值": { text: '危急值', iconText: '危', color: 'red' },
		"检验完成": { text: '√检验完成', iconText: '√', color: 'green' },
		"急诊": { text: '急诊', iconText: '急', color: 'red' },
		"特殊样本": { text: '特殊样本', iconText: '特', color: 'red' },
		"阳性保卡": { text: '阳性保卡', iconText: '阳', color: '#F08080' },
		'打印': { type: 'other', text: '打印', iconText: '<i class="layui-icon layui-icon-print"></i>', color: '#000', background: 'transparent', border: 'none', padding: '0 2px' },
		"智能审核通过": { text: '√智能审核通过', iconText: '√', color: 'green' },
		"智能审核未通过": { text: '×智能审核未通过', iconText: '×', color: 'red' },
		"仪器通过": { text: '√仪器状态通过', iconText: '仪', color: 'green' },
		"仪器警告": { text: '仪器状态警告', iconText: '仪', color: 'orange' },
		"仪器异常": { text: '×仪器状态异常', iconText: '仪', color: 'red' }
	};

	//主状态样式模板
	var MainStatusTemplet = '<span style="margin-right:1px;border:{border};background-color:{backgroundColor};"><a style="color:{color};">{text}</a></span>';
   //颜色条模板
	var ColorLineTemplet = '<span style="padding:0 1px;margin-right:1px;border:1px solid {color};background:{color};"></span>';
 
	//外部接口
	var uxbasic = {
		//状态列显示处理
		onStatusRenderer: function (record,isD) {
			var me = this,
				isD = isD || false,
				html = [];
			//主状态->检0、初2、终3、反(0 and CheckTime<>null)、废-2
			var MainStatusID = isD ? record.LisTestForm_MainStatusID : record.LisTestForm_MainStatusID;
	
			if (MainStatusID == '0') {
				html.push(MainStatusTemplet.replace(/{border}/g, STATUS_LIST[0].border).replace(/{backgroundColor}/g, STATUS_LIST[0].color).replace(/{color}/g, '#fff').replace(/{text}/g, STATUS_LIST[0].iconText));

//				html.push(MainStatusTemplet.replace(/{color}/g, STATUS_LIST[0].color).replace(/{text}/g, STATUS_LIST[0].iconText));
			} else if (MainStatusID == '2') {
				html.push(MainStatusTemplet.replace(/{color}/g, STATUS_LIST[1].color).replace(/{text}/g, STATUS_LIST[1].iconText));
			} else if (MainStatusID == '3') {
				html.push(MainStatusTemplet.replace(/{color}/g, STATUS_LIST[2].color).replace(/{text}/g, STATUS_LIST[2].iconText));
			} else if (MainStatusID == '-2') {
				html.push(MainStatusTemplet.replace(/{color}/g, STATUS_LIST[4].color).replace(/{text}/g, STATUS_LIST[4].iconText));
			}
	
			//危急值->ReportStatusID,倒数第2位=1
			var ReportStatusID = isD ? record.LisTestForm_DReportStatusID + '' : record.LisTestForm_ReportStatusID + '';
			if (ReportStatusID && ReportStatusID.length >= 2 && ReportStatusID.charAt(ReportStatusID.length - 2) == '1') {
				html.push(MainStatusTemplet.replace(/{color}/g, ColorMap.危急值.color).replace(/{text}/g, ColorMap.危急值.iconText));
			}
	
			//检验完成->TestAllStatus,0:未开始;1:已开始;2:检验完成:绿色;
			var TestAllStatus = isD ? record.LisTestForm_DTestAllStatus : record.LisTestForm_TestAllStatus;
			if (TestAllStatus == '1') {
				html.push(MainStatusTemplet.replace(/{color}/g, ColorMap.检验完成.color).replace(/{text}/g, ColorMap.检验完成.iconText));
			}
	
			//复检->RedoStatus,0/1
			var RedoStatus = isD ? record.LisTestForm_DRedoStatus : record.LisTestForm_RedoStatus;
			if (RedoStatus == '1') {
				html.push(ColorLineTemplet.replace(/{color}/g, STATUS_LIST[5].color));
			}
	
			//仪器状态->ESendStatus,0:无;1:通过 绿色;2:警告 黄色;3:异常 红色;
			var ESendStatus = isD ? record.LisTestForm_DESendStatus+ '' : record.LisTestForm_ESendStatus + '';
			if (ESendStatus == '1') {
				html.push(ColorLineTemplet.replace(/{color}/g, ColorMap.仪器通过.color));
			} else if (ESendStatus == '2') {
				html.push(ColorLineTemplet.replace(/{color}/g, ColorMap.仪器警告.color));
			} else if (ESendStatus == '3') {
				html.push(ColorLineTemplet.replace(/{color}/g, ColorMap.仪器异常.color));
			}
	
			//阳性保卡->ReportStatusID,倒数第1位=1
			var ReportStatusID = isD ? record.LisTestForm_DReportStatusID + '' : record.LisTestForm_ReportStatusID + '';
			if (ReportStatusID && ReportStatusID.length >= 1 && ReportStatusID.charAt(ReportStatusID.length - 1) == '1') {
				html.push(ColorLineTemplet.replace(/{color}/g, ColorMap.阳性保卡.color));
			}
	
			//打印->PrintCount>0
	        var PrintCount = record.LisTestForm_PrintCount;
	        if (PrintCount && PrintCount > 0) {
	            //html.push(me.PrintTemplet);
	            html.push(MainStatusTemplet.replace(/{border}/g, ColorMap.打印.border).replace(/{backgroundColor}/g, ColorMap.打印.background).replace(/{color}/g, ColorMap.打印.color).replace(/{text}/g, ColorMap.打印.iconText));
	        }
			return html.join('');
		},
		
		//根据指定样本号批量生成新样本号
	    batchCreateSampleNo:function(curSampleNo,SampleNoCount,callback){
	    	if(!SampleNoCount)SampleNoCount=0;
			var params = JSON.stringify({curSampleNo:curSampleNo,SampleNoCount:Number(SampleNoCount)});
			//显示遮罩层
			var config = {
				type: "POST",
				url: batchCreateSampleNoUrl,
				data: params
			};
			var index = layer.load();
			uxutil.server.ajax(config, function(data) {
				layer.close(index);
				if (data.success) {
					callback(data.ResultDataValue);
				} else {
					uxbase.MSG.onError(data.ErrorInfo);
				}
			});
		},
		//查询截止样本号
		endSampleNo : function(GSampleNo,Num,callback){
			var me = this;
		    me.batchCreateSampleNo(GSampleNo,Num,function(data){
		    	var arr = [],
					val="";
				if(data){
					arr = data.split(',');
    		        val = arr.length>0 ? arr[arr.length-1] : arr[0];
				}
		    	callback(val);
		    });
		},
		// 校验是否是数字，true:数值型的，false：非数值型
	    IsNumber:function(value) {
	        var patrn = /^(-)?\d+(\.\d+)?$/;
		    if (patrn.exec(value) == null || value == "") {
		        return false
		    } else {
		        return true
		    }
	    },
	    //初始化时间组件
	    initComDate : function(elem,range){
	    	//检测日期 yyyy-MM-dd
	        laydate.render({//存在默认值
	            elem: '#'+elem,
	            eventElem:'#'+elem+'+i.layui-icon',
	            type: 'date',
	            range: range,
	            show:true,
	            done: function (value, date, endDate) { }
	        });
	    },
	    //监听日期控件（加图片）
	    initDate:function(elem,defaultvalue,formelem,range){
	    	var me = this;
	    	 //赋值日期框
	        $("#"+elem).val(defaultvalue);
	        //监听日期图标
	        $("#"+elem+"+i.layui-icon.layui-icon-date").on("click", function () {
	            var elemID = $(this).prev().attr("id");
	            if ($("#" + elemID).hasClass("layui-disabled")) return false;
	            var key = $("#" + elemID).attr("lay-key");
	            if ($('#layui-laydate' + key).length > 0) {
	                $("#" + elemID).attr("data-type", "date");
	            } else {
	                $("#" + elemID).attr("data-type", "text");
	            }
	            var datatype = $("#" + elemID).attr("data-type");
	            if (datatype == "text") {
	                me.initComDate(elemID,range);
	                $("#" + elemID).attr("data-type", "date");
	            } else {
	                $("#" + elemID).attr("data-type", "text");
	                var key = $("#" + elemID).attr("lay-key");
	                $('#layui-laydate' + key).remove();
	            }
            
	        });
	         //监听日期input -- 不弹出日期框
	        $("#"+formelem).on('focus', '#'+elem, function () {
	            var device = layui.device();
	            if (device.ie) {
	                window.event.returnValue = false;
	            } else {
	                window.event.preventDefault();
	            }
	            layui.stope(window.event);
	            return false;
	        });
	    },
	    //验证日期是否正确
	    isDateValid : function (obj){
	        //验证日期是否正确
	        var msg = "";
	        if (obj.GTestDate.indexOf(" - ") == -1) {
	            msg = "日期格式不正确!";
	        }
	        //验证是否都是日期
	        var start = obj.GTestDate.split(" - ")[0],
	            end = obj.GTestDate.split(" - ")[1],
	            DATE_FORMAT = /^[0-9]{4}-[0-1]?[0-9]{1}-[0-3]?[0-9]{1}$/; //判断是否是日期格式
	        if (!uxutil.date.isValid(start) || !DATE_FORMAT.test(start) || !uxutil.date.isValid(end) || !DATE_FORMAT.test(end)) {
	            msg = "日期格式不正确!";
	        }
	        //验证开始日期是否大于结束日期
	        //uxutil.date.difference()
	        if (new Date(start).getTime() > new Date(end).getTime()) {
	            msg = "开始日期不能大于结束日期!";
	        }
	        return msg;
	    },
	    //检验日期+样本号排序
	    ascsort : function(a,b){
		    if (a["LisTestForm_GTestDate"] === b["LisTestForm_GTestDate"]) {
		        if (a["LisTestForm_GSampleNoForOrder"] > b["LisTestForm_GSampleNoForOrder"]) {
		            return 1;
		        } else if (a["LisTestForm_GSampleNoForOrder"] < b["LisTestForm_GSampleNoForOrder"]) {
		            return - 1;
		        } else {
		            return 0;
		        }
		    } else {
		        if (a["LisTestForm_GTestDate"] > b["LisTestForm_GTestDate"]) {
		            return 1;
		        } else {
		            return - 1;
		        }
		    }
		},
	    //检验日期+样本号排序(倒序)
	    descsort : function(a,b){
		    if (a["LisTestForm_GTestDate"] === b["LisTestForm_GTestDate"]) {
		        if (a["LisTestForm_GSampleNoForOrder"] < b["LisTestForm_GSampleNoForOrder"]) {
		            return 1;
		        } else if (a["LisTestForm_GSampleNoForOrder"] > b["LisTestForm_GSampleNoForOrder"]) {
		            return - 1;
		        } else {
		            return 0;
		        }
		    } else {
		        if (a["LisTestForm_GTestDate"] < b["LisTestForm_GTestDate"]) {
		            return 1;
		        } else {
		            return - 1;
		        }
		    }
		},
	    getStoreList : function(recs,direction){
		    var me = this,
			    arr = [];
	
			for(var i=0;i<recs.length;i++){
				arr.push(recs[i]);
			}
			var sortList = [];
			if(direction == 'asc')sortList = arr.sort(me.ascsort);
			if(direction=='desc')sortList = arr.sort(me.descsort);
			return sortList;
		},
		/***默认选择行
		 * @description 默认选中并触发行单击处理 
		 * @param that:当前操作实例对象
		 * @param rowIndex: 指定选中的行
		 * */
		doAutoSelect : function(that, rowIndex) {
			var me = this;	
			var data = table.cache[that.instance.key] || [];
			if (!data || data.length <= 0) return;
	
			rowIndex = rowIndex || 0;
			var filter = that.elem.attr('lay-filter');
			var thatrow = $(that.instance.layBody[0]).find('tr:eq(' + rowIndex + ')');
			var cellTop11 = thatrow.offset().top;
			$(that.instance.layBody[0]).scrollTop(cellTop11 - 160);
	
			var obj = {
				tr: thatrow,
				data: data[rowIndex] || {},
				del: function() {
					table.cache[that.instance.key][index] = [];
					tr.remove();
					that.instance.scrollPatch();
				},
				updte: {}
			};
			setTimeout(function() {
				layui.event.call(thatrow, 'table', 'row' + '(' + filter + ')', obj);
			}, 100);
		}
	}
	//暴露接口
	exports('uxbasic',uxbasic);
});