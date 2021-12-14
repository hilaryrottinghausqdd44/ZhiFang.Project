/**
 * 部门数据
 * @author guohaixiang
 * @version 2020-04-09
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
			Id:"HRDept_Id",
			IsUse:'HRDept_IsUse'
        },
        current: null,
        delIndex: null,
        addUrl: uxutil.path.ROOT +'/ServerWCF/Customization/RBACCloneService.svc/SYNCDeptByLIIPGoToLabStar6',
        selectUrl: uxutil.path.ROOT + '/ServerWCF/RBACService.svc/RBAC_UDTO_SearchHRDeptByHQL?isPlanish=true',
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
                    field: 'HRDept_Address',
                    title: 'Address',
                    minWidth: 130,
                    edit: 'text'
                },
                {
                    field: 'HRDept_CName',
                    title: 'CName',
                    minWidth: 130,
                    edit: 'text'
                },
                {
                    field: 'HRDept_Comment',
                    title: 'Comment',
                    minWidth: 130,
                    edit: 'text'
                },
                {
                    field: 'HRDept_Contact',
                    title: 'Contact',
                    minWidth: 130,
                    edit: 'text'
                },
                {
                    field: 'HRDept_DeveCode',
                    title: 'DeveCode',
                    minWidth: 130,
                    edit: 'text'
                },
                {
                    field: 'HRDept_DispOrder',
                    title: 'DispOrder',
                    minWidth: 130,
                    edit: true
                },
                {
                    field: 'HRDept_EName',
                    title: 'EName',
                    minWidth: 130,
                    edit: 'text'
                },
                {
                    field: 'HRDept_Fax',
                    title: 'Fax',
                    minWidth: 130,
                    edit: 'text'
                },
                {
                    field: 'HRDept_LevelNum',
                    title: 'LevelNum',
                    minWidth: 130,
                    edit: 'text'
                },
                {
                    field: 'HRDept_ManagerID',
                    title: 'ManagerID',
                    minWidth: 130,
                    //sort: true,
                    edit: 'text'
                },
                {
                    field: 'HRDept_ManagerName',
                    title: 'ManagerName',
                    minWidth: 130,
                    //sort: true,
                    edit: 'text'
                },
                {
                    field: 'HRDept_OrgCode',
                    title: 'OrgCode',
                    minWidth: 130,
                    //sort: true,
                    edit: 'text'
                },
                {
                    field: 'HRDept_ParentID',
                    title: 'ParentID',
                    minWidth: 130,
                    //sort: true,
                    edit: 'text'
                },
                {
                    field: 'HRDept_ParentOrg',
                    title: 'ParentOrg',
                    minWidth: 130,
                    //sort: true,
                    edit: 'text'
                },
                {
                    field: 'HRDept_PinYinZiTou',
                    title: 'PinYinZiTou',
                    minWidth: 130,
                    //sort: true,
                    edit: 'text'
                },
                {
                    field: 'HRDept_SName',
                    title: 'SName',
                    minWidth: 130,
                    //sort: true,
                    edit: 'text'
                },
                {
                    field: 'HRDept_Shortcode',
                    title: 'Shortcode',
                    minWidth: 130,
                    //sort: true,
                    edit: 'text'
                },
                {
                    field: 'HRDept_StandCode',
                    title: 'StandCode',
                    minWidth: 130,
                    //sort: true,
                    edit: 'text'
                },
                {
                    field: 'HRDept_Tel',
                    title: 'Tel',
                    minWidth: 130,
                    //sort: true,
                    edit: 'text'
                },
                {
                    field: 'HRDept_UseCode',
                    title: 'UseCode',
                    minWidth: 130,
                    //sort: true,
                    edit: 'text'
                },
                {
                    field: 'HRDept_ZipCode',
                    title: 'ZipCode',
                    minWidth: 130,
                    //sort: true,
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
                    "count": data.count || 0, //解析数据长度
                    "data": data.list || []
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
            	var deptarr = [];
                $.each(tabledata, function (i, item) {
                	var dept = {};
	            	if(item.HRDept_IsUse == "true"){
	            		dept.HRDept_IsUse = true;
	            	}else if(item.HRDept_IsUse == "false"){
	            		dept.HRDept_IsUse = false;
	            	}
	            	dept.HRDept_DispOrder = parseInt(item.HRDept_DispOrder);
	            	dept.HRDept_Id = parseInt(item.HRDept_Id);
	            	dept.HRDept_CName = item.HRDept_CName;
	            	dept.HRDept_SName = item.HRDept_SName;
	            	dept.HRDept_Shortcode = item.HRDept_Shortcode;
	            	dept.HRDept_StandCode = item.HRDept_StandCode;
	            	dept.HRDept_DeveCode= item.HRDept_DeveCode;
	            	deptarr.push(dept);
	            });
	            var data = { entity: deptarr };
                uxutil.server.ajax({
                    url: tableObj.addUrl,
                    type: "POST",
                    data: JSON.stringify(data).replace(/HRDept_/g,"")
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