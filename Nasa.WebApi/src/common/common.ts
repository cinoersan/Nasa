export interface ErrorData{
    message :string;
}

export interface CommonResult<T> {
    isSuccess: boolean,
    data: T | null | ErrorData
}



export const fetchCommon = async <T>(fullUrl: string, options?: object): Promise<CommonResult<T>> => {

    if (!options) {
        options = {
            method: "POST",
        };
    }


    let finalOptions = { ...options };

    let result: CommonResult<T> = {
        isSuccess: true,
        data: null
    }

    try {
        const res = await fetch(fullUrl, finalOptions);
        const pathName = window.location.pathname;
        if (res.status === 401 && pathName !== "/" && pathName !== '/login') {
            window.location.href = "/";
        }

        const json = await res.json();

        if (res.status !== 200) {
            result.isSuccess = false;
            result.data = {
                message: json.message
            }
            
        }

        result.data = json;

    } catch (err) {
        result.isSuccess = false;  
        result.data = {
            message: `FE error! => ${err || ''}`
        };
    }
    return result;
};
