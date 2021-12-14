/**
 * 区县字典
 * @author guohaixiang
 * @version 2020-03-17
 */

layui.extend({
    uxutil: 'ux/util'
}).use(['uxutil', 'table'], function () {
	var layer = layui.layer,
		uxutil = layui.uxutil,
		$ = layui.jquery;
	//表格	
	var tableObj = {
		table: layui.table,
		fields:{
            Id:"SLIIPSystemRBACCloneLog_Id"
        },
        current: null,
        delIndex: null,
        selectUrl: uxutil.path.ROOT + '/ServerWCF/Customization/RBACCloneService.svc/ST_UDTO_SearchSLIIPSystemRBACCloneLogByHQL?isPlanish=true&fields=' + getStoreFields(),
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
                    sort: true,
                    hide: true
                },
                {
                    field: 'SLIIPSystemRBACCloneLog_OperName',
                    title: '同步人名称',
                    minWidth: 130
                },
                {
                    field: 'SLIIPSystemRBACCloneLog_SystemCode',
                    title: '系统编码',
                    minWidth: 130
                },
                {
                    field: 'SLIIPSystemRBACCloneLog_SystemName',
                    title: '系统名称',
                    minWidth: 70
                },
                {
                    field: 'SLIIPSystemRBACCloneLog_DataName',
                    title: '同步数据对象',
                    minWidth: 100
                },
                {
                    field: 'SLIIPSystemRBACCloneLog_DataJson',
                    title: '同步数据内容',
                    minWidth: 100
                }
                ]
            ],
            page: true,
            limit: 50,
            limits: [50, 100, 200, 500, 1000],
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
    };
    /**创建数据字段*/
    function getStoreFields() {
        var fields = [];
        $(":input").each(function () {
            if (this.name) fields.push(this.name)
        });
        return fields.join(',');
    };
	
	//table上面的工具栏
	tableObj.table.on('toolbar(table)', function(obj) {
		switch(obj.event) {			
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
                        var where = " and SystemCode like '%" + val + "%' or OperName like '%" + val + "%' or SystemName like '%" + val + "%' or DataName like '%" + val + "%'";
                        url = tableObj.selectUrl.replace(')', where);
                    } else {
                        url = encodeURI(tableObj.selectUrl + "&where=SystemCode like '%" + val + "%' or OperName like '%" + val + "%' or SystemName like '%" + val + "%' or DataName like '%" + val + "%'");
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
                var where = " and SystemCode like '%" + val + "%' or OperName like '%" + val + "%' or SystemName like '%" + val + "%' or DataName like '%" + val + "%'";
                url = url.replace(')', where);
            } else {
                url = encodeURI(url + "&where=SystemCode like '%" + val + "%' or OperName like '%" + val + "%' or SystemName like '%" + val + "%' or DataName like '%" + val + "%'");
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