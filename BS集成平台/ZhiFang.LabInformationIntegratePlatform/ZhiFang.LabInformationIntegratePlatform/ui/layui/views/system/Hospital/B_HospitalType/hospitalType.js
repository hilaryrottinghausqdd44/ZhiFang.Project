/**
 * 临床诊断
 * @author guohaixiang
 * @version 2019-12-10
 */

layui.extend({
    uxutil: 'ux/util'
}).use(['uxutil', 'table', 'form'], function () {
	var layer = layui.layer,
		uxutil = layui.uxutil,
		$ = layui.jquery;
	//表格	
	var tableObj = {
		table: layui.table,
		form: layui.form,
		fields:{
			Id:"BHospitalType_Id",
			IsUse:'BHospitalType_IsUse'
        },
        current: null,
        delIndex: null,
        addUrl: uxutil.path.ROOT +'/ServerWCF/LIIPCommonService.svc/ST_UDTO_AddBHospitalType',
        selectUrl: uxutil.path.ROOT + '/ServerWCF/LIIPCommonService.svc/ST_UDTO_SearchBHospitalTypeByHQL?isPlanish=true',
        updateUrl: uxutil.path.ROOT +'/ServerWCF/LIIPCommonService.svc/ST_UDTO_UpdateBHospitalTypeByField',
        delUrl: uxutil.path.ROOT +'/ServerWCF/LIIPCommonService.svc/ST_UDTO_DelBHospitalType',
        checkRowData: [], //选中数据
        refresh: function () {
            var searchText = $("#search")[0].value;
            tableObj.table.reload('table', {
                where: {
                    time: new Date().getTime()
                }
            });
            tableObj.checkRowData = [];
            if (searchText != "") {
                $("#search").val(searchText);
            }
        }
	};
	//表单
	var formObj = {
		form: layui.form,
		type: 'edit',
        reset: function () { //重置
            if (formObj.type == 'edit') {
                if (tableObj.checkRowData.length > 0) {
                    formObj.form.val("form", tableObj.checkRowData[tableObj.checkRowData.length - 1]);
                    if (tableObj.checkRowData[tableObj.checkRowData.length - 1].BHospitalType_IsUse == "true") {
                        if (!$("#isUse").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
                            $("#isUse").next('.layui-form-switch').addClass('layui-form-onswitch');
                            $("#isUse").next('.layui-form-switch').children("em").html("启用");
                            $("#isUse")[0].checked = true;
                        }
                    } else {
                        if ($("#isUse").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
                            $("#isUse").next('.layui-form-switch').removeClass('layui-form-onswitch');
                            $("#isUse").next('.layui-form-switch').children("em").html("禁用");
                            $("#isUse")[0].checked = false;
                        }
                    }
                }
            } else {
                $("#layForm")[0].reset();
                if (!$("#isUse").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
                    $("#isUse").next('.layui-form-switch').addClass('layui-form-onswitch');
                    $("#isUse").next('.layui-form-switch').children("em").html("启用");
                    $("#isUse")[0].checked = true;
                }
            }
        },
		empty: function() { //清空
            $("#layForm")[0].reset();
            if (!$("#isUse").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
                $("#isUse").next('.layui-form-switch').addClass('layui-form-onswitch');
                $("#isUse").next('.layui-form-switch').children("em").html("启用");
                $("#isUse")[0].checked = true;
            }
		},
        disabledSaveBtn: function () {
            if (!$('#save').hasClass('layui-btn-disabled')) {
                $("#save").prop("disabled", "disabled");
                $('#save').addClass('layui-btn-disabled');
            }
        },
        enableSaveBtn: function () {
            if ($('#save').hasClass('layui-btn-disabled')) {
                $("#save").removeProp("disabled");
                $('#save').removeClass('layui-btn-disabled');
            }
        }
	};
    init();
    function init() {
        $(".tableHeight").css("height", ($(window).height() - 30) + "px");//设置表单容器高度
            tableRender();
    }
	//初始化表格
    function tableRender() {
        tableObj.table.render({
            elem: '#table',
            height: 'full-50',
            //size: 'sm', //小尺寸的表格
            defaultToolbar: ['filter'],
            toolbar: '#toolbar',
            url: tableObj.selectUrl,
            cols: [
                [{
                    type: 'numbers',title:'序号'
                },
                {
                    field: tableObj.fields.Id,
                    width: 60,
                    title: '主键ID',
                    sort: true,
                    hide: true
                },
                {
                    field: 'BHospitalType_Name',
                    title: '名称',
                    minWidth: 130,
                    sort: true
                },
                {
                    field: 'BHospitalType_Code',
                    title: '代码',
                    minWidth: 130,
                    sort: true
                },
                {
                    field: 'BHospitalType_SName',
                    title: '简称',
                    minWidth: 130,
                    sort: true
                },
                {
                    field: 'BHospitalType_Shortcode',
                    title: '快捷码',
                    minWidth: 130,
                    //sort: true
                },
                {
                    field: 'BHospitalType_PinYinZiTou',
                    title: '拼音字头',
                    minWidth: 130,
                    //sort: true
                },
                {
                    field: 'BHospitalType_Comment',
                    title: '备注',
                    //sort: true,
                    minWidth: 70
                },
                {
                    field: tableObj.fields.IsUse,
                    title: '是否使用',
                    minWidth: 100,
                    templet: '#switchTp',
                    unresize: true
                }
                ]
            ],
            page: false,
            limit: 99999999,
            //limits: [50, 100, 200, 500, 1000],
            autoSort: false, //禁用前端自动排序
            done: function (res, curr, count) {
                if (count > 0) {
                    $("#table+div .layui-table-body table.layui-table tbody tr:first-child")[0].click();
                }
            },
            response: function () {
                return {
                    statusCode: true, //成功状态码
                    statusName: 'code', //code key
                    msgName: 'msg ', //msg key
                    dataName: 'data' //data key
                }
            },
            parseData: function (res) {//res即为原始返回的数据
                if (!res) return;
                var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
                return {
                    "code": res.success ? 0 : 1, //解析接口状态
                    "msg": res.ErrorInfo, //解析提示文本
                    "count": data.count || 0, //解析数据长度
                    "data": data.list || []
                };
            },
            text: {
                none: '暂无相关数据'
            }
        });
    }
	//监听table启用禁用/显示隐藏操作
	tableObj.form.on('switch(tableSwitch)', function(obj) {
		var name = obj.elem.name;
		var value = obj.elem.checked;
        var Id = obj.othis[0].parentElement.parentElement.parentElement.children[1].innerText;
        var loadIndex = layer.load();//开启加载层
		$.ajax({
			type:'post',
			dataType:'json',
			contentType: "application/json",
			data:JSON.stringify({entity:{Id:Id,IsUse:value},fields:'Id,IsUse'}),
			url:tableObj.updateUrl,
            success: function (res) {
                layer.close(loadIndex);//关闭加载层
				if(res.success){
					//更新右边表单和本行数据
					var arr = tableObj.table.cache.table;
					for(var i in arr){
						if(arr[i][tableObj.fields.Id] == Id){
							arr[i][tableObj.fields.IsUse] = value.toString();
						}
					}
					if(tableObj.checkRowData.length > 0){
						for(var j in tableObj.checkRowData){
							if(tableObj.checkRowData[j][tableObj.fields.Id] == Id){
								tableObj.checkRowData[j][tableObj.fields.IsUse] = value;
								if(j == tableObj.checkRowData.length - 1){
                                    if (value) {
                                        if (!$("#isUse").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
                                            $("#isUse").next('.layui-form-switch').addClass('layui-form-onswitch');
                                            $("#isUse").next('.layui-form-switch').children("em").html("启用");
                                            $("#isUse")[0].checked = true;
                                        }
                                    } else {
                                        if ($("#isUse").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
                                            $("#isUse").next('.layui-form-switch').removeClass('layui-form-onswitch');
                                            $("#isUse").next('.layui-form-switch').children("em").html("禁用");
                                            $("#isUse")[0].checked = false;
                                        }
                                    }
								}
							}
						}
					}
				}else{
					tableObj.refresh();
					if(value){
						layer.msg("启用失败",{icon: 5,anim:6});
					}else{
						layer.msg("禁用失败",{icon: 5,anim:6});
					}
				}
			}
		});
		layui.stope(window.event);
	});
	//table上面的工具栏
	tableObj.table.on('toolbar(table)', function(obj) {
		switch(obj.event) {
			case 'add':
                if (tableObj.checkRowData.length > 0) {
                    formObj.type = 'add';//编辑还是新增
                    formObj.reset();
                    formObj.enableSaveBtn();
                } else {
                    formObj.type = 'add';//编辑还是新增
                    formObj.enableSaveBtn();
                }
				break;
			case 'edit':
				if(tableObj.checkRowData.length === 0) {
					layer.msg('请选择一行！');
				} else {
					formObj.enableSaveBtn();
					formObj.type = 'edit';//编辑还是新增
					formObj.reset();
				}
				break;
			case 'del':
				if(tableObj.checkRowData.length === 0) {
					layer.msg('请选择一行！');
               } else {
                    layer.confirm('确定删除选中项?', { icon: 3, title: '提示' }, function (index) {
                        var loadIndex = layer.load();//开启加载层
						var len = tableObj.checkRowData.length;
					  	for(var i = 0; i < tableObj.checkRowData.length; i++){
					  		var Id = tableObj.checkRowData[i][tableObj.fields.Id];
					  		$.ajax({
						  		type:"get",
						  		url:tableObj.delUrl+"?Id="+Id,
						  		async:true,
						  		dataType:'json',
                                success: function (res) {
						  			if(res.success){
						  				len--;
                                        if (len == 0) {
                                            layer.close(loadIndex);//关闭加载层
							  				layer.close(index);
											layer.msg("删除成功！",{icon: 6,anim:0});
                                            tableObj.refresh();
										  	formObj.disabledSaveBtn();
										  	formObj.empty();
							  			}
						  			}else{
						  				layer.msg("删除失败！", { icon: 5, anim: 6 });
                                        tableObj.delIndex = null;
                                        layer.close(loadIndex);//关闭加载层
						  			}
						  		}
						  	});
					  	}
					});
				}
				break;
			case 'search':
				if($("#search")[0].value == ""){
					tableObj.table.reload('table',{
                        url: tableObj.selectUrl,
                        where: {
                            time: new Date().getTime()
                        }
					});
					tableObj.checkRowData = [];
				}else{
					var val = $("#search")[0].value;
					var url = "";
                    if (tableObj.selectUrl.indexOf("where") != -1) {
                        var where = " and Name like '%" + val + "%' or SName like '%" + val + "%' " + " or Shortcode like '%" + val + "%' or PinYinZiTou like '%" + val + "%'";
                        url = tableObj.selectUrl.replace(')', where);
                    } else {
                        url = encodeURI(tableObj.selectUrl + "&where=Name like '%" + val + "%' or SName like '%" + val + "%'" + " or Shortcode like '%" + val + "%' or PinYinZiTou like '%" + val + "%'");
                    }
					tableObj.table.reload('table',{
                        url: url,
                        where: {
                            time: new Date().getTime()
                        }
					});
					tableObj.checkRowData = [];
					$("#search").val(val);
				}
		};
	});
    //监听行单击事件
    tableObj.table.on('row(table)', function (obj) {
        tableObj.checkRowData = [];
        formObj.type = 'edit';
        tableObj.checkRowData.push(obj.data);
        //标注选中样式
        obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
        formObj.reset();
        formObj.disabledSaveBtn();
    });
	//监听form表单
	formObj.form.on('submit(save)', function(data) {
        window.event.preventDefault();
        var loadIndex = layer.load();//开启加载层
		var fields = "";//发送修改的字段
		var postData = {};//发送的数据
		//判断复选框是否选中 未选中的话手动添加字段
		if(!$("#isUse").next('.layui-form-switch').hasClass('layui-form-onswitch')){
			data.field[tableObj.fields.IsUse] = "false"
		}else{
			data.field[tableObj.fields.IsUse] = "true"
		}
		for(k in data.field){
			postData[k.split("_")[1]] = data.field[k];
			fields += k.split("_")[1]+",";
		}
		fields = fields.slice(0,fields.length-1);
		if(formObj.type == 'edit') {
			$.ajax({
				type:'post',
				dataType:'json',
				contentType: "application/json",
				data:JSON.stringify({entity:postData,fields:fields}),
				url:tableObj.updateUrl,
                success: function (res) {
                    layer.close(loadIndex);//关闭加载层
					if(res.success){
						layer.msg("编辑成功！",{icon: 6,anim:0});
						formObj.disabledSaveBtn();
                        formObj.empty();
						tableObj.refresh();
					}else{
						layer.msg("编辑失败！",{icon: 5,anim:6});
					}
				}
			});
		} else if(formObj.type == 'add') {
			delete postData.Id;
			$.ajax({
				type:'post',
				dataType:'json',
				contentType: "application/json",
				data:JSON.stringify({entity:postData}),
				url:tableObj.addUrl,
                success: function (res) {
                    layer.close(loadIndex);//关闭加载层
					if(res.success){
						layer.msg("新增成功！",{icon: 6,anim:0});
						formObj.disabledSaveBtn();
                        formObj.empty();
                        var data = eval('(' + res.ResultDataValue + ')');
						tableObj.refresh();
					}else{
						layer.msg("新增失败！",{icon: 5,anim:6});
					}
				}
			});
		}
	});
	//重置添加、修改不同操作
	$('#cancel').on('click', function() {
		if(tableObj.checkRowData.length == 0){
			formObj.empty();
			return;
		}
		if(formObj.type == 'edit') {
			formObj.reset();
		} else if(formObj.type == 'add') {
			formObj.empty();
		}
	});
    
    //监听排序事件
    tableObj.table.on('sort(table)', function (obj) {
        var field = obj.field;//排序字段
        var type = obj.type;//升序还是降序
        var url = tableObj.selectUrl;
        if(type == null){
        	tableObj.table.reload('table', {
	            initSort: obj, //记录初始排序，如果不设的话，将无法标记表头的排序状态
	            url: url,
	            where: {
	                time: new Date().getTime()
	            }
	        });
	        if (val != "") {
	            $("#search").val(val);
	        }
            return;
        }
        if (url.indexOf("sort") != -1) {//存在
            var start = url.indexOf("sort=[");
            var end = url.indexOf("]") + 1;
            var oldStr = url.slice(start, end);
            var newStr = 'sort=[{property:"' + field + '",direction:"' + type + '"}]';
            url = url.replace(oldStr, newStr);
        } else {
            url = url + '&sort=[{property:"' + field + '",direction:"' + type + '"}]';
        }
        tableObj.selectUrl = url;//修改默认的排序字段
        tableObj.checkRowData = [];
        var val = $("#search")[0].value;
        if (val != "") {
            if (url.indexOf("where") != -1) {
                var where = " and CName like '%" + val + "%' or SName like '%" + val + "%'"  + " or Shortcode like '%" + val + "%' or PinYinZiTou like '%" + val + "%'";
                url = url.replace(')', where);
            } else {
                url = encodeURI(url + "&where=CName like '%" + val + "%' or SName like '%" + val + "%'" + " or Shortcode like '%" + val + "%' or PinYinZiTou like '%" + val + "%'");
            }
        }
        tableObj.table.reload('table', {
            initSort: obj, //记录初始排序，如果不设的话，将无法标记表头的排序状态
            url: url,
            where: {
                time: new Date().getTime()
            }
        });
        if (val != "") {
            $("#search").val(val);
        }
    });
    //监听浏览器窗口
    window.onresize = function () {
        $(".tableHeight").css("height", ($(window).height() - 30) + "px");//设置表单容器高度
    };

});