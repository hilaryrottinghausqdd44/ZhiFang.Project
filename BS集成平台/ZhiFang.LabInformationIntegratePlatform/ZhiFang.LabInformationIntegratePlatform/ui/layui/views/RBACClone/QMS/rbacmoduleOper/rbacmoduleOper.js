/**
 * 模块操作数据
 * @author guohaixiang
 * @version 2020-03-27
 */

layui.extend({
    uxutil: 'ux/util'
}).use(['uxutil', 'table', 'form', 'layer'], function () {
	var layer = layui.layer,
		uxutil = layui.uxutil,
		$ = layui.jquery;
	//表格	
	var tableObj = {
		table: layui.table,
		form: layui.form,
		fields:{
			Id:"Id",
			IsUse:'IsUse'
        },
        current: null,
        delIndex: null,
        addUrl: uxutil.path.ROOT +'/ServerWCF/Customization/RBACCloneService.svc/SYNCRBACModuleOperByQMS',
        selectUrl: uxutil.path.ROOT + '/ServerWCF/Customization/RBACCloneService.svc/CatchRBACModuleOperDataListByQMS',
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
            size: 'sm', //小尺寸的表格
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
                    hide: true
                },
                {
                    field: 'RBACModule_Id',
                    title: 'RBACModule_Id',
                    minWidth: 130
                },
                {
                    field: 'RBACRowFilter_Id',
                    title: 'RBACRowFilter_Id',
                    minWidth: 130
                },
                {
                    field: 'UseCode',
                    title: 'UseCode',
                    minWidth: 130,
                    edit: 'text'
                },
                {
                    field: 'StandCode',
                    title: 'StandCode',
                    minWidth: 130,
                    edit: 'text'
                },
                {
                    field: 'DeveCode',
                    title: 'DeveCode',
                    minWidth: 130,
                    edit: 'text'
                },
                {
                    field: 'CName',
                    title: 'CName',
                    minWidth: 130,
                    edit: 'text'
                },
                {
                    field: 'EName',
                    title: 'EName',
                    minWidth: 130,
                    edit: true
                },
                {
                    field: 'SName',
                    title: 'SName',
                    minWidth: 130,
                    edit: 'text'
                },
                {
                    field: 'Shortcode',
                    title: 'Shortcode',
                    minWidth: 130,
                    edit: 'text'
                },
                {
                    field: 'PinYinZiTou',
                    title: 'PinYinZiTou',
                    minWidth: 130,
                    edit: 'text'
                },
                {
                    field: 'Comment',
                    title: 'Comment',
                    minWidth: 130,
                    edit: 'text'
                },
                {
                    field: 'DispOrder',
                    title: 'DispOrder',
                    minWidth: 130,
                    edit: 'text'
                },
                {
                    field: 'OperateURL',
                    title: 'OperateURL',
                    minWidth: 130,
                    edit: 'text'
                },
                {
                    field: 'RowFilterURL',
                    title: 'RowFilterURL',
                    minWidth: 130,
                    edit: 'text'
                },
                {
                    field: 'RowFilterBase',
                    title: 'RowFilterBase',
                    minWidth: 130,
                    edit: 'text'
                },
                {
                    field: 'FilterCondition',
                    title: 'FilterCondition',
                    minWidth: 130,
                    edit: 'text'
                },
                {
                    field: 'ColFilterURL',
                    title: 'ColFilterURL',
                    minWidth: 130,
                    edit: 'text'
                },
                {
                    field: 'ColFilterBase',
                    title: 'ColFilterBase',
                    minWidth: 130,
                    edit: 'text'
                },
                {
                    field: 'ColFilterDesc',
                    title: 'ColFilterDesc',
                    minWidth: 130,
                    edit: 'text'
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
                    "count": data.length || 0, //解析数据长度
                    "data": data || []
                };
            },
            text: {
                none: '暂无相关数据'
            }
        });
    };
	
	//table上面的工具栏
	tableObj.table.on('toolbar(table)', function(obj) {
		switch(obj.event) {
            case 'add':
                var tabledata = tableObj.table.cache.table;
                $.each(tabledata, function (i, item) {
	            	if(item.DataAddTime){
	            		item.DataAddTime = uxutil.date.toServerDate(item.DataAddTime);
	            	}
	            	if(item.DataTimeStamp && typeof item.DataTimeStamp == "string"){
	            		var str   = item.DataTimeStamp.split(',');
	                    var bytes = [];
	                    for (var i = 0;i<str.length;i++) {
	                    	bytes.push(str[i]);
	                    }						
						item.DataTimeStamp = bytes; 
	            	}                    
	            });
	            var data = { entity: tabledata };
                uxutil.server.ajax({
                    url: tableObj.addUrl,
                    type: "POST",
                    data: JSON.stringify(data)
                }, function (data) {
                        if (data) {
                            if (data.success) {
                                layer.msg('同步成功！');
                            } else {
                                layer.msg(data.ErrorInfo, { icon: 5, anim: 6 });
                            }
                        } else {
                            layer.msg('同步异常！');
                        }
                });
				break;
		};
    });
    //监听浏览器窗口
    window.onresize = function () {
        $(".tableHeight").css("height", ($(window).height() - 30) + "px");//设置表单容器高度
    };   
    
});