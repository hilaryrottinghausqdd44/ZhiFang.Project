$(function () {
    //页面所有功能对象
    var shell_win = {
        selectempid:[],
        selectempname:[],
        Load: function () {
            if (shell_win.selectempid.length > 0)
            {
                for(var i=0;i<shell_win.selectempid.length;i++)
                {
                    //alert(shell_win.selectempid[i]);
                    if ($("#checked" + shell_win.selectempid[i]).attr("src") == "../img/icon/unchecked.png")
                    {
                        $("#checked" + shell_win.selectempid[i]).attr("src", "../img/icon/checked.png");
                    }
                }
            }
        },
        Submit: function () {
            //alert(shell_win.selectempid.length);
            //alert(shell_win.selectempid[0]);

            if (shell_win.selectempid.length > 0) {
                //alert(shell_win.selectempid.join(','));
            }
            if (shell_win.selectempname.length > 0) {
                //alert(shell_win.selectempname.join(','));
            }
            //parent.document.getElementById("todaycontentmemo").value = shell_win.selectempid.join(',') + '---' + shell_win.selectempname.join(',');

            //parent.SetCopyForList(shell_win.selectempid.join(','), shell_win.selectempname.join(','));
            //alert(localStorage.getItem("WorkLogTypeListChoosebackpageurl"));
            //alert(shell_win.selectempid);
            Shell.util.Cookie.setCookie("ChooseEmpIdList", shell_win.selectempid);
            Shell.util.Cookie.setCookie("ChooseEmpNameList", shell_win.selectempname);
            //alert("shell_win.selectempname:" + shell_win.selectempname);
            //alert(Shell.util.Cookie.getCookie("ChooseEmpNameList"));
            if (Shell.util.Cookie.getCookie("ChooseEmpbackpageurl")) {
                location.href = Shell.util.Cookie.getCookie("ChooseEmpbackpageurl") + "";
            }
            else {
                //history.go(-1);
                history.back();
            }
        },
        setcheckstatus: function (empidid) {
            //alert(1);
            if ($("#checked" + empidid)) {
                //alert(2);                
                if ($("#checked" + empidid).attr("src") == "../img/icon/unchecked.png") {                   
                    var indexid = jQuery.inArray(empidid, shell_win.selectempid)
                    if (indexid < 0)
                    {
                        //alert(3);
                        if (shell_win.selectempid.length > 0) {
                            for (var i = 0; i < shell_win.selectempid.length; i++) {
                                //alert(i + "@" + shell_win.selectempid[i]);
                                $("#checked" + shell_win.selectempid[i]).attr("src", "../img/icon/unchecked.png");
                            }
                        }
                        shell_win.selectempid = [];
                        shell_win.selectempid.push(empidid);
                        // alert(4);
                        if ($("#empname" + empidid)) {
                            //alert(5);
                            shell_win.selectempname = [];
                            shell_win.selectempname.push($("#empname" + empidid).text())
                        }
                    }
                    //alert(6);
                    $("#checked" + empidid).attr("src", "../img/icon/checked.png");
                    //alert(7);

                }
                else {
                   // alert(jQuery.inArray(empidid, shell_win.selectempid));//是b这个元素在数组arrList 中的位置  splice(index,1)
                    var indexid = jQuery.inArray(empidid, shell_win.selectempid)
                    if (indexid >= 0) {
                        shell_win.selectempid.splice(indexid, 1)
                    }
                    if ($("#empname" + empidid)) {
                        var indexname = jQuery.inArray($("#empname" + empidid).text(), shell_win.selectempname)
                        if (indexname >= 0) {
                            shell_win.selectempname.splice(indexname, 1)
                        }
                    }                    
                    $("#checked" + empidid).attr("src", "../img/icon/unchecked.png");
                }
            }
            //alert(shell_win.selectempid.join(','));
            //alert(shell_win.selectempname.join(','));
        },
        /**初始化*/
        init: function () {
            if (Shell.util.Cookie.getCookie("ChooseEmpIdList")) {
                //alert(Shell.util.Cookie.getCookie("ChooseEmpIdList"));
                shell_win.selectempid = Shell.util.Cookie.getCookie("ChooseEmpIdList").split(',');
                //alert(shell_win.selectempid.length);
            }

            if (Shell.util.Cookie.getCookie("ChooseEmpNameList")) {
                shell_win.selectempname = Shell.util.Cookie.getCookie("ChooseEmpNameList").split(',');
            }

            $(".num_checkbox").on(Shell.util.Event.touch, function () {
                shell_win.setcheckstatus($(this).attr('empid'));
                //alert($(this).attr('id'));
            });
            $("#submitspan").on(Shell.util.Event.touch, shell_win.Submit);
            shell_win.Load();
            //alert("EmpNames=" + p["EmpNames"]);
        }
    };
    shell_win.init();
});
