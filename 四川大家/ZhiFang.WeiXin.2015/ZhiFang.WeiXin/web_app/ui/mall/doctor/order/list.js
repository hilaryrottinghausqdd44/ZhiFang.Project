$(function() {
	//获取医嘱单服务地址
	var GET_DOCTOR_ORDER_LIST_URL = JcallShell.System.Path.ROOT + "/ServerWCF/ZhiFangWeiXinAppDoctService.svc/WXADS_BA_SearchDoctorOrderForm";

	//获取列表数据
	function getDoctorOrderListData(callback) {
		var url = GET_DOCTOR_ORDER_LIST_URL;
		url += '?page=1&limit=20'

		$("#loading-div").modal({ backdrop: 'static', keyboard: false });
		
		var data = {
			page:1,
			limit:20
		};
		data = JcallShell.JSON.encode(data);
		//获取数据
		JcallShell.Server.ajax({
			url: url,
			type:'post',
			data:data
		}, function(data) {
			$("#loading-div").modal("hide");
			callback(data);
		});
	}
	//更改列表内容
	function changeDoctorOrderListHtml(list) {
		var templet = getRowTemplet(),
			defaultPic = JcallShell.System.Path.ROOT + '/Images/dajia/logo.jpg',
			html = [];
		//VO:ZhiFang.WeiXin.Entity.ViewObject.Response.BLabTestItemVO
		for(var i in list) {
			var row = templet;
			row = row.replace(/{Hospital}/g, list[i].HN);
			row = row.replace(/{Dept}/g, list[i].DPN);
			row = row.replace(/{Doctor}/g, list[i].DN);
			row = row.replace(/{Patient}/g, list[i].UN);
			row = row.replace(/{Sex}/g, list[i].SD == "1" ? "男" : "女");
			row = row.replace(/{Age}/g, list[i].Age);
			row = row.replace(/{AgeUnit}/g, list[i].AUN);
			row = row.replace(/{Id}/g, list[i].Id);
			row = row.replace(/{DataAddTime}/g, list[i].DataAddTime);
			
			//获取医嘱单状态信息
			JcallShell.LocalStorage.Enum.getInfoById('DoctorOrderFormStatus',list[i].SS,function(info){
				row = row.replace(/{StatusColor}/g, info.FontColor);
				row = row.replace(/{StatusBgColor}/g, info.BGColor);
				row = row.replace(/{StatusName}/g, info.Name);
			});
			
			html.push(row);
		}

		//html.push('<div><div style="">加载更多</div></div>');

		$("#list").html(html.join(""));
	}
	//获取列表行模板
	function getRowTemplet() {
		var templet =
			'<div class="list-div">' +
				'<div style="float:left;padding-left:5px;">' +
					'<div style="color:#169ada;"><b>{Hospital}-{Dept}</b></div>' +
					'<div>医生：{Doctor} </div>' +
					'<div>患者：{Patient} {Sex} {Age}{AgeUnit} </div>' +
	                '<div >开单时间：{DataAddTime}</div>'+
	            '</div>' +
	            '<div style="float:left;width:100%;">' +
					'<div class="list-div-button" style="margin-top:-60px;" onclick="showInfo(\'{Id}\')">详情</div>' +
					'<div class="list-div-button" style="margin-top:-25px;padding:2px 5px;font-size:12px;border-radius:0;' +
						'color:{StatusColor};background-color:{StatusBgColor}">{StatusName}' +
					'</div>' +
				'</div>' +
			'</div>';
		return templet;
	}
	//显示错误信息
	function showError(msg) {
		$("#list").html('<div style="margin:20px 10px;color:#169ada;text-align:center;">' + msg + '</div>');
	}

	function showInfo(id) {
		//跳转到信息页面
		location.href = "info.html?id=" + id;
	}
	window.showInfo = showInfo;
	//初始化页面
	function initHtml() {
		//获取医嘱单状态信息列表-初始化
		JcallShell.LocalStorage.Enum.getListByClassName('DoctorOrderFormStatus',function(info){
			//获取列表数据
			getDoctorOrderListData(function(data) {
				if(data.success == true) {
					//更改列表内容
					var list = data.value ? (data.value.list || []) : [];
					if(list.length == 0){
						showError("没有医嘱单信息！");
					}else{
						changeDoctorOrderListHtml(list);
					}
				} else {
					showError(data.msg);
				}
			});
		});
	}
	initHtml();
});