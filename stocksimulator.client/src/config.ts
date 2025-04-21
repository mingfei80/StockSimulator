export const API_BASE_URL = {
    local: 'https://localhost:7112',
    DEV: 'https://dev.api.example.com',
    UAT: 'https://uat.api.example.com',
    LIVE: 'https://live.api.example.com',
};

// You can switch the environment here manually, or later automate it with `.env` files
export const CURRENT_ENV = 'local';

export const BASE_URL = API_BASE_URL[CURRENT_ENV];