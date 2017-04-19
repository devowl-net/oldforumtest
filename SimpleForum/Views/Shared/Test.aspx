<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>Test</title>
    <script src="<%= Url.Content("~/Scripts/jquery-1.8.2.js") %>" > </script>
    <%--<script src="//ajax.googleapis.com/ajax/libs/prototype/1.7.1.0/prototype.js" />--%>
</head>
<body>
    <div>
        <% using (Html.BeginForm("Test", "AjaxTest", FormMethod.Post, new { onSubmit = "return verify(this);" }))
           { %>
        <script>
            function verify(form) {
                //debugger;
                
                if (typeof sending === "undefined")
                {
                    sending = false;
                }

                if (sending) return false;
                sending = true;

                var teststr = document.getElementsByName("teststr")[0].value
                if (teststr) {
                    $.ajax(
                    {
                        type: 'POST',
                        url: '<%= Url.Action("CheckTestString") %>',
                        data: { 'teststr': teststr },
                        error: function (xhr, ajaxOptions, thrownError) {
                            //debugger;
                            sending = false;
                            alert(xhr.status);
                            alert(xhr.responseText);
                            alert(thrownError);
                        },
                        success: function (msg) {
                            //debugger;
                            sending = false;
                            if (msg == true) {
                                form.submit();
                            }
                            else {
                                alert('Ошибка!');
                            }
                        }
                    });
                }

                return false;
            }
        </script>
            <h1>TestStr</h1>
            <input type="text" name="teststr" />
            <input type="submit" class="button" value="Отправка" >
        <% } %>
    </div>
</body>
</html>
