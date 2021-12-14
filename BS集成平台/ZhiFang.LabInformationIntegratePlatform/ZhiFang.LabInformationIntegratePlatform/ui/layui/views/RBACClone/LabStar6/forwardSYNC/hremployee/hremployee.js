/**
 * 员工数据
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
			Id:"HREmployee_Id",
			IsUse:'HREmployee_IsUse'
        },
        current: null,
        delIndex: null,
        selectUrl: uxutil.path.ROOT + '/ServerWCF/RBACService.svc/RBAC_UDTO_SearchHREmployeeByHQL?isPlanish=true',
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
            id:'talbe',
            cols: [
                [{ type: 'checkbox' },{
                    type: 'numbers',title:'序号'
                },
                {
                    field: tableObj.fields.Id,
                    width: 60,
                    title: '主键ID',
                    hide: true
                },
                {
                    field: 'HREmployee_Address',
                    title: 'Address',
                    minWidth: 130,
                    edit: 'text'
                },
                {
                    field: 'HREmployee_CName',
                    title: 'CName',
                    minWidth: 130,
                    edit: 'text'
                },
                {
                    field: 'HREmployee_Comment',
                    title: 'Comment',
                    minWidth: 130,
                    edit: 'text'
                },
                {
                    field: 'HREmployee_ContinuingEducation',
                    title: 'ContinuingEducation',
                    minWidth: 130,
                    edit: 'text'
                },
                {
                    field: 'HREmployee_DeveCode',
                    title: 'DeveCode',
                    minWidth: 130,
                    edit: 'text'
                },
                {
                    field: 'HREmployee_DispOrder',
                    title: 'DispOrder',
                    minWidth: 130,
                    edit: true
                },
                {
                    field: 'HREmployee_EName',
                    title: 'EName',
                    minWidth: 130,
                    edit: 'text'
                },
                {
                    field: 'HREmployee_ExtTel',
                    title: 'ExtTel',
                    minWidth: 130,
                    edit: 'text'
                },
                {
                    field: 'HREmployee_Family',
                    title: 'Family',
                    minWidth: 130,
                    edit: 'text'
                },
                {
                    field: 'HREmployee_GraduateSchool',
                    title: 'GraduateSchool',
                    minWidth: 130,
                    edit: 'text'
                },
                {
                    field: 'HREmployee_HomeTel',
                    title: 'HomeTel',
                    minWidth: 130,
                    //sort: true,
                    edit: 'text'
                },
                {
                    field: 'HREmployee_IdNumber',
                    title: 'IdNumber',
                    minWidth: 130,
                    //sort: true,
                    edit: 'text'
                },
                {
                    field: 'HREmployee_NameF',
                    title: 'NameF',
                    minWidth: 130,
                    //sort: true,
                    edit: 'text'
                },
                {
                    field: 'HREmployee_NameL',
                    title: 'NameL',
                    minWidth: 130,
                    //sort: true,
                    edit: 'text'
                },
                {
                    field: 'HREmployee_OfficeTel',
                    title: 'OfficeTel',
                    minWidth: 130,
                    //sort: true,
                    edit: 'text'
                },
                {
                    field: 'HREmployee_PinYinZiTou',
                    title: 'PinYinZiTou',
                    minWidth: 130,
                    //sort: true,
                    edit: 'text'
                },
                {
                    field: 'HREmployee_SName',
                    title: 'SName',
                    minWidth: 130,
                    //sort: true,
                    edit: 'text'
                },
                {
                    field: 'HREmployee_Shortcode',
                    title: 'Shortcode',
                    minWidth: 130,
                    //sort: true,
                    edit: 'text'
                },
                {
                    field: 'HREmployee_StandCode',
                    title: 'StandCode',
                    minWidth: 130,
                    //sort: true,
                    edit: 'text'
                },
                {
                    field: 'HREmployee_Tel',
                    title: 'Tel',
                    minWidth: 130,
                    //sort: true,
                    edit: 'text'
                },
                {
                    field: 'HREmployee_UseCode',
                    title: 'UseCode',
                    minWidth: 130,
                    //sort: true,
                    edit: 'text'
                },
                {
                    field: 'HREmployee_ZipCode',
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
                var checkStatus = tableObj.table.checkStatus('talbe'),
                    data = checkStatus.data;
                    
                if(data.length <= 0){
                	layer.msg("请选择数据");
                	return;
                }
            	var emparr = [];
                $.each(data, function (i, item) {
                	var emp = {};
	            	if(item.HREmployee_IsUse == "true"){
	            		emp.HREmployee_IsUse = true;
	            	}else if(item.HRDept_IsUse == "false"){
	            		emp.HREmployee_IsUse = false;
	            	}
	            	emp.HREmployee_DispOrder = parseInt(item.HREmployee_DispOrder);
	            	emp.HREmployee_Id = parseInt(item.HREmployee_Id);
	            	emp.HREmployee_CName = item.HREmployee_CName;
	            	emp.HREmployee_SName = item.HREmployee_SName;
	            	emp.HREmployee_Shortcode = item.HREmployee_Shortcode;
	            	emp.HREmployee_StandCode = item.HREmployee_StandCode;
	            	emp.HREmployee_DeveCode= item.HREmployee_DeveCode;
	            	emp.HREmployee_Comment= item.HREmployee_Comment;
	            	emparr.push(emp);
	            });
	            var enttrylist = { entity: emparr };
	            var  tabletype= $(parent.window.document.body).find("#checkboxs>input[type=checkbox]:checked");
	            if(tabletype.length <= 0){
	            	layer.msg("请选择要向哪些表同步");
	            	return;
	            }
	            for(var i = 0;i<tabletype.length;i++ ){
	            	switch (tabletype[i].name) {
			            case "Doctor":
			                 url = uxutil.path.ROOT + '/ServerWCF/Customization/RBACCloneService.svc/SYNCDoctortByLIIPGoToLabStar6';
			                 syncdata(url,"Doctor",enttrylist);
			                 break;
			            case "Puser":
			                url = uxutil.path.ROOT + '/ServerWCF/Customization/RBACCloneService.svc/SYNCPUserByLIIPGoToLabStar6';
			                syncdata(url,"Puser",enttrylist);
			                break;
		                case "NPuser":
		                	url = uxutil.path.ROOT + '/ServerWCF/Customization/RBACCloneService.svc/SYNCNPUserByLIIPGoToLabStar6';
		                	syncdata(url,"NPuser",enttrylist);
		                 	break;
		        	}	
	            }
				break;
		};
    });
    
    function syncdata(url,type,enttrylist){
    	uxutil.server.ajax({
            url: url,
            type: "POST",
            data: JSON.stringify(enttrylist).replace(/HREmployee_/g,"")
        }, function (data) {
                if (data) {
                    if (data.success) {
                        layer.msg(type+'同步成功！');
                    } else {
                        layer.msg(data.ErrorInfo, { icon: 5, anim: 6 });
                    }
                } else {
                    layer.msg(type+'同步异常！');
                }
        });    	
    };
    //监听浏览器窗口
    window.onresize = function () {
        $(".tableHeight").css("height", ($(window).height() - 30) + "px");//设置表单容器高度
    };   
    
});