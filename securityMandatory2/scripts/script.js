(function () {
    "use strict";

    function modal() {

        var init = function () {
            $(".btnChatroomAll").addClass("glbHide");
            $(".chat").addClass("glbHide");
            $(".messageSender").addClass("messageSenderHide");;
            $(".messageThread").addClass("messageThreadHide");

            var chatRoomId = $("#MsgChatRoomId").val();
            if (chatRoomId != 0) {
                $(".chatroom").addClass("chatRoomHide");
                $("#modal-chatmail").removeClass("glbHide");;
                $("#chatroomselect-" + chatRoomId).removeClass("chatRoomHide");;
                $("#chatroomselect-" + chatRoomId).addClass("active");;
                $("#chatroom-" + chatRoomId).removeClass("glbHide");
                $(".btnChatroomAll").removeClass("glbHide");;
                $(".messageThreadHide").removeClass("messageThreadHide");
                $(".messageSenderHide").removeClass("messageSenderHide");

                if ($('.messageThread').length) {
                    var wtf = $('.messageThread');
                    var height = wtf[0].scrollHeight - 40;
                    wtf.scrollTop(height);
                }
            }


            $(".btnMenu").on("click", function (e) {
                $(".courses").addClass("courseHide");
                $(".btnMenu").removeClass("btnActive");
                $(this).addClass("btnActive");
                $("#" + $(this).attr("data-modal")).removeClass("courseHide");;
            });

            $(".btnChatroomAll").on("click", function (e) {
                $(".btnChatroomAll").addClass("glbHide");
                $(".chat").addClass("glbHide");
                $(".messageSender").addClass("messageSenderHide");
                $(".messageThread").addClass("messageThreadHide");
                $(".chatroom").removeClass("chatRoomHide");;
            });

            $(".chatroom").on("click", function (e) {
                $(".chatroom").addClass("chatRoomHide");
                $(".chat").addClass("glbHide");
                $(this).addClass("active");;
                $(this).removeClass("chatRoomHide");;
                $(".btnChatroomAll").removeClass("glbHide");;
                var chatroom = $(this).attr("data-modal");
                var chatroomId = parseInt(chatroom.match(/[0-9]+/)[0], 10);

                $("#" + chatroom).removeClass("glbHide");;
                $(".messageSender").removeClass("messageSenderHide");;
                $(".messageThread").removeClass("messageThreadHide");;
                $("#MsgChatRoomId").val(chatroomId);

                if ($('.messageThread').length) {
                    var wtf = $('.messageThread');
                    var height = wtf[0].scrollHeight - 40;
                    wtf.scrollTop(height);
                }

            });
        }

        return {
            init: init
        };
    }

    modal = modal();
    modal.init();

})();