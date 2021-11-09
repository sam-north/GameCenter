export function isNullOrWhiteSpace(str: string) : Boolean {
    return (!str || /^\s*$/.test(str));
}