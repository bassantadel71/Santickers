import { environment } from '../../environments/environment';

export function apiImageUrl(url: string | null | undefined): string {
  if (!url) return '';
  if (/^https?:\/\//.test(url)) return url;
  const base = environment.apiUrl.replace(/\/api\/?$/, '');
  return `${base}${url.startsWith('/') ? url : `/${url}`}`;
}