$(document).ready(function () {
    $("#submitBtn").on("click", function (event) {
        $(".error-message").remove();
        var isValid = true;

        // valid-m-8 數字8位數 可空
        $("[class*='valid-m-']").each(function () {
            var inputElement = $(this);
            var inputVal = inputElement.val();
            var classList = inputElement.attr("class").split(/\s+/);
            var digitLength = 0;

            for (var i = 0; i < classList.length; i++) {
                if (classList[i].startsWith("valid-m-")) {
                    digitLength = parseInt(classList[i].replace("valid-m-", ""));
                    break;
                }
            }

            var regex = new RegExp("^\\d{" + digitLength + "}$");
            if (inputVal !== "" && !regex.test(inputVal)) {
                var errorElement = $("<div class='alert-danger error-message'>必須是 " + digitLength + " 位數字</div>");
                inputElement.after(errorElement);
                isValid = false;
            }
        });

        // valid-mr-8 數字必填8位數
        $("[class*='valid-mr-']").each(function () {
            var inputElement = $(this);
            var inputVal = inputElement.val();
            var classList = inputElement.attr("class").split(/\s+/);
            var digitLength = 0;
            var isRequired = false;
            var regex = null;

            for (var i = 0; i < classList.length; i++) {
                if (classList[i].startsWith("valid-mr-")) {
                    digitLength = parseInt(classList[i].replace("valid-mr-", ""));
                    isRequired = true;
                    break;
                }
            }

            if (isRequired && inputVal === "") {
                var errorElement = $("<div class='alert-danger error-message'>此欄位為必填</div>");
                inputElement.after(errorElement);
                isValid = false;
            } else if (inputVal !== "") {
                regex = new RegExp("^\\d{" + digitLength + "}$");

                if (!regex.test(inputVal)) {
                    var errorElement = $("<div class='alert-danger error-message'>必須是 " + digitLength + " 位數字</div>");
                    inputElement.after(errorElement);
                    isValid = false;
                }
            }
        });

        // valid-a-10 全部10位數以內
        $("[class*='valid-a-']").each(function () {
            var inputElement = $(this);
            var inputVal = inputElement.val();
            var classList = inputElement.attr("class").split(/\s+/);
            var maxLength = 0;

            for (var i = 0; i < classList.length; i++) {
                if (classList[i].startsWith("valid-a-")) {
                    maxLength = parseInt(classList[i].replace("valid-a-", ""));
                    break;
                }
            }

            if (inputVal.length > maxLength) {
                var errorElement = $("<div class='alert-danger error-message'>不能超過 " + maxLength + " 字元</div>");
                inputElement.after(errorElement);
                isValid = false;
            }
        });

        // valid-ar-10 全部必填10位數以內
        $("[class*='valid-ar-']").each(function () {
            var inputElement = $(this);
            var inputVal = inputElement.val();
            var classList = inputElement.attr("class").split(/\s+/);
            var maxLength = 0;
            var isRequired = false;

            for (var i = 0; i < classList.length; i++) {
                if (classList[i].startsWith("valid-ar-")) {
                    maxLength = parseInt(classList[i].replace("valid-ar-", ""));
                    isRequired = true;
                    break;
                }
            }

            if (isRequired && inputVal === "") {
                var errorElement = $("<div class='alert-danger error-message'>此欄位為必填</div>");
                inputElement.after(errorElement);
                isValid = false;
            } else if (inputVal.length > maxLength) {
                var errorElement = $("<div class='alert-danger error-message'>不能超過 " + maxLength + "字元</div>");
                inputElement.after(errorElement);
                isValid = false;
            }
        });

        // validDate-r 必填檢查
        $("[class*='validDate-r']").each(function () {
            var inputElement = $(this);
            var inputVal = inputElement.val();

            if (inputVal === "") {
                var errorElement = $("<div class='alert-danger error-message'>此欄位為必填</div>");
                inputElement.after(errorElement);
                isValid = false;
            } else if (!checkValidDate(inputVal)) {
                var errorElement = $("<div class='alert-danger error-message'>請輸入有效的日期</div>");
                inputElement.after(errorElement);
                isValid = false;
            }
        });

        // validDate 檢查（可空）
        $("[class*='validDate-a']").each(function () {
            var inputElement = $(this);
            var inputVal = inputElement.val();

            if (inputVal !== "" && !checkValidDate(inputVal)) {
                var errorElement = $("<div class='alert-danger error-message'>請輸入有效的日期</div>");
                inputElement.after(errorElement);
                isValid = false;
            }
        });

        if (!isValid) {
            event.preventDefault();
        }
    });

    // 檢查日期格式及有效性
    function checkValidDate(date) {
        // 檢查是否為8位數字
        if (!/^\d{8}$/.test(date)) {
            return false;
        }

        var year = parseInt(date.substring(0, 4), 10);
        var month = parseInt(date.substring(4, 6), 10);
        var day = parseInt(date.substring(6, 8), 10);

        if (month < 1 || month > 12) {
            return false;
        }

        var daysInMonth = new Date(year, month, 0).getDate(); 
        if (day < 1 || day > daysInMonth) {
            return false;
        }

        return true; 
    }
});
