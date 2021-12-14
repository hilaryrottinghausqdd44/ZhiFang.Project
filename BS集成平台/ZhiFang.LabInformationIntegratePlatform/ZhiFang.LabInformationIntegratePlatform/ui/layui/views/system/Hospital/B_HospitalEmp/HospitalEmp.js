layui.extend({
    uxutil: 'ux/util',
    uxtable: 'ux/table'
}).use(['uxutil', 'uxtable', 'form'], function () {
    var $ = layui.$,
        form = layui.form,
        laydate = layui.laydate,
        uxtable = layui.uxtable,
        uxutil = layui.uxutil;

    //获取人员列表
    //http://localhost/ZhiFang.LabInformationIntegratePlatform/ServerWCF/RBACService.svc/RBAC_UDTO_SearchRBACUserByHQL?page=1&limit=10&fields=RBACUser_Account,RBACUser_HREmployee_CName,RBACUser_HREmployee_StandCode,RBACUser_HREmployee_Id&where=IsUse=1&sort=[{"property":"RBACUser_DispOrder","direction":"asc"}]&isPlanish=true
    var GET_EMP_LIST_URL = uxutil.path.ROOT + "/ServerWCF/RBACService.svc/RBAC_UDTO_SearchRBACUserByHQL";
    //获取待选/已选医院列表
    var GET_UNSELECTHOSPITAL_URL = uxutil.path.ROOT + "/ServerWCF/LIIPCommonService.svc/ST_UDTO_SearchSelectHospitalListByEmpId";
    //删除员工医院关系服务
    var DEL_EMPHOSPITAL_URL = uxutil.path.ROOT + "/ServerWCF/LIIPCommonService.svc/ST_UDTO_DelBHospitalEmpLink";
    //员工医院关系服务
    var SetLinkType_EMPHOSPITAL_URL = uxutil.path.ROOT + "/ServerWCF/LIIPCommonService.svc/BHospitalEmpLinkSetLinkType";
    //新增员工医院关系界面
    var UnSelectHospital_URL = "UnSelectHospital.html";

    var tmpempid = "";
    //人员列表
    var config_table_emp = {
        elem: $("#tableemp"),
        toolbar: '#toolbarEmp',
        height: 'full-50',
        defaultLoad: true,
        page: {
            limit: 50,
            limits: [50, 100, 200]
        },
        initSort: {
            field: 'RBACUser_DispOrder',//排序字段
            type: 'asc'
        },
        cols: [[
            { field: 'RBACUser_HREmployee_Id', width: 1, title: '人员Id', hide: true },
            { field: 'RBACUser_HREmployee_CName', width: '35%', title: '人员名称', sort: true },
            { field: 'RBACUser_Account', width: '35%', title: '登录帐号', sort: true },
            { field: 'RBACUser_HREmployee_StandCode', width: '30%', title: '编码', sort: true }
        ]]
    };
    var tableInd = uxtable.render(config_table_emp);
    //头工具栏事件
    tableInd.table.on('toolbar(tableemp)', function (obj) {
        switch (obj.event) {
            case 'search': onSearch(); break;
        }
    });
    //监听员工行点击事件
    tableInd.table.on('row(tableemp)', function (obj) {
        //alert(obj.data.RBACUser_HREmployee_Id);
        loadHospitalList(obj.data);
    });


    //
    var config_table_hospital = {
        elem: $("#tablehospital"),
        id: "tablehospital",
        toolbar: '#toolbarHospital',
        height: 'full-50',
        defaultLoad: true,
        page: {
            limit: 50,
            limits: [50, 100, 200]
        },
        initSort: {
            field: 'RBACUser_DispOrder',//排序字段
            type: 'asc'
        },
        cols: [[
            { type: 'checkbox' },
            { field: 'BHospitalEmpLink_Id', width: 1, title: '关系Id', hide: true },
            { field: 'BHospitalEmpLink_HospitalName', width: '45%', title: '医院名称', sort: true },
            { field: 'BHospitalEmpLink_HospitalCode', width: '30%', title: '医院编码', sort: true },
            //{ field: 'BHospitalEmpLink_LinkTypeName', width: '15%', title: '关系类型' },
            {
                field: "BHospitalEmpLink_LinkTypeName",
                title: '关系类型',
                minWidth: 100,
                templet: '#switchTp',
                unresize: true
            },
            { fixed: 'right', title: '操作', toolbar: '#RowBtn', width: 80 },
            { field: 'BHospitalEmpLink_HospitalID', width: 100, title: '医院Id', hide: true }
        ]]
    };
    var tableInd_hospital = uxtable.render(config_table_hospital);
    //头工具栏事件
    tableInd_hospital.table.on('toolbar(tablehospital)', function (obj) {
        console.log(obj);
        switch (obj.event) {
            case 'search': onSearch(); break;
            case 'add': addhospital(); break;
            case 'alldel': alldelhospital(); break;
        }
    });
    function alldelhospital() {
        var checkStatus = tableInd_hospital.table.checkStatus('tablehospital'); 
        var clns = checkStatus.data.length;
        if (clns<=0) {
            layer.msg('请选择数据！');
        } else {
            var index = 0;
            for (var i = 0; i < checkStatus.data.length; i++) {
                $.ajax({
                    type: 'get',
                    contentType: 'application/json',
                    url: DEL_EMPHOSPITAL_URL + '?id=' + checkStatus.data[i].BHospitalEmpLink_Id,
                    success: function (data) {
                        index++;
                        if (index==i) {
                            loadHospitalListByEmpId(tmpempid);
                        }
                    }
                });
            }
        }
    };
    //监听行工具事件
    tableInd_hospital.table.on('tool(tablehospital)', function (obj) {
        switch (obj.event) {
            case 'del': layer.confirm('确定是否删除？', function (index) {
                //alert(obj.data.BHospitalEmpLink_Id);
                delhospitalemplink(obj.data.BHospitalEmpLink_Id);
            }); break;
        }
    });

    //监听切换关系类型操作
    form.on('switch(LinkType)', function (obj) {
        layer.tips(this.value + ' ' + this.name + '：' + obj.elem.checked, obj.othis);
        var typeid = (obj.elem.checked) ?'1':'2';
        $.ajax({
            type: 'get',
            contentType: 'application/json',
            url: SetLinkType_EMPHOSPITAL_URL + '?id=' + this.value + '&typeid=' + typeid,
            success: function (data) {
                if (data.success == true) {
                    layer.msg('操作成功！');
                } else {
                    layer.msg('操作失败！');
                }
                loadHospitalListByEmpId(tmpempid);
            }
        });
    });

    //查询
    function onSearch() {
        var searchText = $("#searchtextemp").val(), where = [];

        //条件
        where.push(" IsUse=1 ");
        if (searchText && searchText != "")
            where.push(" (rbacuser.HREmployee.CName like '%" + searchText + "%' or rbacuser.Account like '%" + searchText + "%')");

        loadEmpList({ "where": where.join(' and ') });

    };
    //添加人员医院关系页面
    function addhospital() {
        var empid = tmpempid;
        if (empid == 0 || empid == "") {
            layer.msg('请选择左侧人员后再添加医院！');
        }
        else {
            var url = UnSelectHospital_URL;
            if (url) {
                layer.open({
                    title: '选择医院',
                    type: 2,
                    content: url + '?EMPID=' + empid + '&t=' + new Date().getTime(),
                    maxmin: true,
                    toolbar: true,
                    resize: true,
                    area: ['850px', '500px']
                });
            }
        }
    };

    //获取人员列表
    function loadEmpList(whereObj, callback) {

        var cols = config_table_emp.cols[0],
            fields = [];

        for (var i in cols) {
            fields.push(cols[i].field);
        }

        tableInd.reload({
            url: GET_EMP_LIST_URL,
            where: $.extend({}, whereObj, {
                isPlanish: true,
                fields: fields.join(',')
            })
        });
    };

    function loadHospitalList(empdata, callback) {

        if (empdata && empdata.RBACUser_HREmployee_Id != "") {
            tmpempid = empdata.RBACUser_HREmployee_Id;
            loadHospitalListByEmpId(empdata.RBACUser_HREmployee_Id);
        }
    };

    function loadHospitalListByEmpId(empid, callback) {

        var cols = config_table_hospital.cols[0],
            fields = [];

        for (var i in cols) {
            fields.push(cols[i].field);
        }
        var eid = 0;
        if (empid && empid!= "") {
            eid = empid;
        }
        tableInd_hospital.reload({
            url: GET_UNSELECTHOSPITAL_URL + '?v=' + uxutil.Random.generateMixed(20),
            where: $.extend({}, "", {
                isPlanish: true,
                fields: fields.join(','),
                flag: true,
                EmpId: eid
            })
        });
    };

    function delhospitalemplink(hospitalemplinkid) {
        $.ajax({
            type: 'get',
            contentType: 'application/json',
            url: DEL_EMPHOSPITAL_URL + '?id=' + hospitalemplinkid,
            success: function (data) {
                if (data.success == true) {
                    layer.msg('操作成功！');
                    loadHospitalListByEmpId(tmpempid);
                } else {
                    layer.msg('所属医院关系删除失败！');
                }
            }
        });
    };
    //初始化
    function init() {
        onSearch();
        loadHospitalList("");
        window.loadHospitalListByEmpId = loadHospitalListByEmpId;
    };

    //初始化
    init();
});