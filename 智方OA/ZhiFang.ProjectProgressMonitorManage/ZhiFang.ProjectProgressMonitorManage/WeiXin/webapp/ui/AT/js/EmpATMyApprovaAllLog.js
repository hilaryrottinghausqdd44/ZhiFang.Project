$(function () {
    //页面所有功能对象
    var shell_win = {
        ApprovalAllLogUnList:"",
        Load: function () {
        },
        ApprovalBatch: function () {
            alert(this.ApprovalAllLogUnList);
        },
        /**初始化*/
        init: function () {
            //alert('123123123');
            //ATCommon.info.show('aaaaaaaaa');
            var p = Shell.util.getRequestParams();
            //alert("all@"+p["ATEventType"]);
            $('#iframe_content').attr("src", "EmpATMyApprovaAllLogUn.html?ATEventType=" + p["ATEventType"]);
            $('#unfinish').on('click', function () {
                $(".active").removeClass('active');
                $('#iframe_content').attr("src", "EmpATMyApprovaAllLogUn.html?ATEventType=" + p["ATEventType"]);
                $('#unfinishl').addClass('active');
            });
            $('#finish').on('click', function () {
                $(".active").removeClass('active');
                $('#iframe_content').attr("src", "EmpATMyApprovaAllLogEd.html?ATEventType=" + p["ATEventType"] );
                $('#finishl').addClass('active');
            });
        }
    };
    shell_win.init();
});