class Service {
  get (url, callBackFunction, errorHandler) {
    this.#fetch(url, 'GET', null, callBackFunction, errorHandler);
  }

  put (url, data, callBackFunction, errorHandler) {
    this.#fetch(url, 'PUT', data, callBackFunction, errorHandler);
  }

  post (url, data, callBackFunction, errorHandler) {
    this.#fetch(url, 'POST', data, callBackFunction, errorHandler);
  }

  delete (url, callBackFunction, errorHandler) {
    this.#fetch(url, 'DELETE', null, callBackFunction, errorHandler);
  }

  #fetch (url, method, data, callBackFunction, errorHandler) {
    const requestOptions = {
      method: method,
      mode: 'cors',
      headers: {
        'Content-Type': 'application/json',
      }
    };

    if (data != null) {
      requestOptions.body = JSON.stringify(data);
    }

    async function fetchAsync() {
      const raw = await fetch(url, requestOptions);
      const response = await raw.json();
      if (response.success) {
        callBackFunction(response.data);
        errorHandler(null);
      }
      else {
        callBackFunction([]);
        errorHandler(response.error);
      }
    };
    fetchAsync();
  }
}

export default Service;