/**
 * Convierte las keys de un objeto de PascalCase a camelCase
 * @param {Object} obj - Objeto a transformar
 * @returns {Object} - Objeto con keys en camelCase
 */
export const convertirACamelCase = (obj) => {
    if (obj === null || obj === undefined) return obj
    if (Array.isArray(obj)) {
        return obj.map(item => convertirACamelCase(item))
    }
    if (typeof obj !== 'object') return obj

    return Object.keys(obj).reduce((acc, key) => {
        const camelKey = key.charAt(0).toLowerCase() + key.slice(1)
        acc[camelKey] = convertirACamelCase(obj[key])
        return acc
    }, {})
}

/**
 * Crea un interceptor de respuesta para axios que convierte PascalCase a camelCase
 * @param {AxiosInstance} axiosInstance - Instancia de axios
 */
export const agregarInterceptorCamelCase = (axiosInstance) => {
    axiosInstance.interceptors.response.use(
        (response) => {
            if (response.data) {
                response.data = convertirACamelCase(response.data)
            }
            return response
        },
        (error) => {
            if (error.response && error.response.data) {
                error.response.data = convertirACamelCase(error.response.data)
            }
            return Promise.reject(error)
        }
    )
}
